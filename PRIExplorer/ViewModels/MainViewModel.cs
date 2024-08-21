using Microsoft.Win32;
using PRIExplorer.Views;
using PriFormat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PRIExplorer.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public FileStream PriStream { get; private set; }
        public PriFile PriFile { get; private set; }
        public string ResourceRootPath { get; private set; }

        public ObservableCollection<EntryViewModel> Entries { get; private set; }
        public ObservableCollection<CandidateViewModel> Candidates { get; private set; }

        object previewContent;

        EntryViewModel selectedEntry;
        CandidateViewModel selectedCandidate;

        public RelayCommand OpenCommand { get; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand SetResourceRootPathCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            OpenCommand = new RelayCommand(OpenCommand_Execute);
            CloseCommand = new RelayCommand(CloseCommand_Execute);
            SetResourceRootPathCommand = new RelayCommand(SetResourceRootPathCommand_CanExecute, SetResourceRootPathCommand_Execute);

            Entries = new ObservableCollection<EntryViewModel>();
            Candidates = new ObservableCollection<CandidateViewModel>();
        }

        private void OpenCommand_Execute()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Package Resource Index files (*.pri)|*.pri";

            if (openFileDialog.ShowDialog() != true)
                return;

            OpenPriFile(openFileDialog.FileName);
        }

        private void CloseCommand_Execute()
        {
            Application.Current.Shutdown();
        }

        private bool SetResourceRootPathCommand_CanExecute()
        {
            return PriFile != null;
        }

        private void SetResourceRootPathCommand_Execute()
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();

            openFolderDialog.Title = "Set resource path root";
            openFolderDialog.Multiselect = false;
            openFolderDialog.InitialDirectory = ResourceRootPath;

            if (openFolderDialog.ShowDialog() != true)
                return;

            ResourceRootPath = openFolderDialog.FolderName;

            GetEntries();
        }

        public void OpenPriFile(string path)
        {
            ClosePriFile();

            try
            {
                PriStream = File.OpenRead(path);

                PriFile = PriFile.Parse(PriStream);
            }
            catch
            {
                ClosePriFile();
                MessageBox.Show("Could not read file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!PriFile.Sections.OfType<ResourceMapSection>().Any())
            {
                ClosePriFile();
                MessageBox.Show("Incompatible PRI file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ResourceRootPath = Path.GetDirectoryName(path);

            GetEntries();

            SetResourceRootPathCommand.RaiseCanExecuteChanged();
        }

        private void ClosePriFile()
        {
            Entries.Clear();
            Candidates.Clear();
            PreviewContent = null;

            if (PriStream != null)
            {
                PriStream.Close();
                PriStream = null;
            }

            PriFile = null;
            ResourceRootPath = null;

            SetResourceRootPathCommand.RaiseCanExecuteChanged();
        }

        private void GetEntries()
        {
            Entries.Clear();
            Candidates.Clear();

            ResourceMapSection primaryResourceMapSection = PriFile.GetSectionByRef(PriFile.PriDescriptorSection.PrimaryResourceMapSection.Value);
            HierarchicalSchemaSection schemaSection = PriFile.GetSectionByRef(primaryResourceMapSection.SchemaSection);

            Dictionary<ResourceMapEntry, EntryViewModel> entriesToViewModels = new Dictionary<ResourceMapEntry, EntryViewModel>();

            bool parentMissing = false;

            do
            {
                foreach (ResourceMapScope scope in schemaSection.Scopes)
                {
                    if (scope.FullName == string.Empty)
                        continue;

                    IList<EntryViewModel> targetEntryCollection;

                    if (scope.Parent == null)
                        targetEntryCollection = Entries;
                    else
                    {
                        EntryViewModel parentViewModel;

                        if (scope.Parent.FullName == string.Empty)
                            targetEntryCollection = Entries;
                        else
                            if (!entriesToViewModels.TryGetValue(scope.Parent, out parentViewModel))
                        {
                            parentMissing = true;
                            continue;
                        }
                        else
                            targetEntryCollection = parentViewModel.Children;
                    }

                    EntryViewModel entry = new EntryViewModel(scope);

                    GetEntryType(entry);

                    entriesToViewModels.Add(scope, entry);

                    targetEntryCollection.Add(entry);
                }
            } while (parentMissing);

            foreach (ResourceMapItem item in schemaSection.Items)
            {
                EntryViewModel parentViewModel;

                if (!entriesToViewModels.TryGetValue(item.Parent, out parentViewModel))
                    continue;

                parentViewModel.Children.Add(new EntryViewModel(item));

                GetEntryType(parentViewModel.Children.Last());
            }

            CollapseStringResources();
        }

        private IEnumerable<Candidate> EnumerateCandidates(ResourceMapItem resourceMapItem)
        {
            ResourceMapSection primaryResourceMapSection =
                PriFile.GetSectionByRef(PriFile.PriDescriptorSection.PrimaryResourceMapSection.Value);

            CandidateSet candidateSet;

            if (primaryResourceMapSection.CandidateSets.TryGetValue(resourceMapItem.Index, out candidateSet))
                foreach (Candidate candidate in candidateSet.Candidates)
                    if (candidate != null)
                        yield return candidate;
        }

        private void GetCandidates(ResourceMapItem resourceMapItem)
        {
            Candidates.Clear();

            foreach (Candidate candidate in EnumerateCandidates(resourceMapItem))
            {
                CandidateViewModel candidateViewModel = new CandidateViewModel(PriFile, PriStream, ResourceRootPath, resourceMapItem, candidate);

                Candidates.Add(candidateViewModel);
            }
        }

        private void GetEntryType(EntryViewModel entry)
        {
            if (entry.ResourceMapEntry is ResourceMapScope)
                entry.Icon = "/Assets/folder-horizontal.png";
            else
            {
                entry.Icon = "/Assets/blue-document.png";

                ResourceMapItem resourceMapItem = (ResourceMapItem)entry.ResourceMapEntry;

                CandidateViewModel[] candidates = EnumerateCandidates(resourceMapItem)
                    .Select(candidate => new CandidateViewModel(PriFile, PriStream, ResourceRootPath, resourceMapItem, candidate)).ToArray();

                if (candidates.Length == 0)
                    entry.Icon = "/Assets/document.png";
                else if (candidates.All(c => c.SourceNotFound || c.LocationNotFound))
                    entry.Icon = "/Assets/blue-document-attribute-x.png";
                else if (candidates.All(
                    c => c.Candidate.Type == ResourceValueType.String ||
                    c.Candidate.Type == ResourceValueType.AsciiString ||
                    c.Candidate.Type == ResourceValueType.Utf8String))
                {
                    entry.Icon = "/Assets/blue-document-attribute-s.png";
                    entry.IsString = true;
                }
                else if (resourceMapItem.Name.EndsWith(".xbf", StringComparison.OrdinalIgnoreCase) ||
                    resourceMapItem.Name.EndsWith(".xaml", StringComparison.OrdinalIgnoreCase))
                    entry.Icon = "/Assets/blue-document-xaml.png";
            }
        }

        private void CollapseStringResources()
        {
            foreach (EntryViewModel entry in Entries)
                if (entry.Type == EntryType.Scope)
                    CollapseStringResources(entry);
        }

        private void CollapseStringResources(EntryViewModel entry)
        {
            if (ContainsOnlyStringResources(entry))
            {
                Dictionary<EntryViewModel, string> strings = new Dictionary<EntryViewModel, string>();
                CollectStringResources(entry, string.Empty, strings);
                entry.Children.Clear();
                foreach (KeyValuePair<EntryViewModel, string> s in strings)
                    entry.Children.Add(new StringEntryViewModel(s.Key.ResourceMapEntry, s.Value));
            }
            else
                foreach (EntryViewModel child in entry.Children)
                    if (child.Type == EntryType.Scope)
                        CollapseStringResources(child);
        }

        private bool ContainsOnlyStringResources(EntryViewModel entry)
        {
            foreach (EntryViewModel child in entry.Children)
            {
                if (child.Type == EntryType.Scope)
                {
                    if (!ContainsOnlyStringResources(child))
                        return false;
                }
                else
                    if (!child.IsString)
                        return false;
            }

            return true;
        }

        private void CollectStringResources(EntryViewModel entry, string prefix, Dictionary<EntryViewModel, string> strings)
        {
            foreach (EntryViewModel child in entry.Children)
                if (child.Type == EntryType.Scope)
                    CollectStringResources(child, (prefix != "" ? (prefix + ".") : "") + child.ResourceMapEntry.Name, strings);
                else
                    strings.Add(child, (prefix != "" ? (prefix + ".") : "") + child.ResourceMapEntry.Name);
        }

        public EntryViewModel SelectedEntry
        {
            get
            {
                return selectedEntry;
            }
            set
            {
                if (selectedEntry != value)
                {
                    selectedEntry = value;
                    SelectedEntryChanged();
                }
            }
        }

        public CandidateViewModel SelectedCandidate
        {
            get
            {
                return selectedCandidate;
            }
            set
            {
                if (selectedCandidate != value)
                {
                    selectedCandidate = value;
                    SelectedCandidateChanged();
                }
            }
        }

        public object PreviewContent
        {
            get
            {
                return previewContent;
            }
            set
            {
                if (previewContent != value)
                {
                    previewContent = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PreviewContent)));
                }
            }
        }

        public void SelectedEntryChanged()
        {
            if (selectedEntry == null)
            {
                PreviewContent = null;
                return;
            }

            //if (selectedEntry.ResourceMapEntry is ResourceMapScope)
            //{
            //    ResourceMapScope selectedScope = (ResourceMapScope)selectedEntry.ResourceMapEntry;

            //    scopeDetailFrame.Navigate(new ScopeDetailPage(PriFile, PriStream, selectedScope));

            //    scopeDetailFrame.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    scopeDetailFrame.Visibility = Visibility.Hidden;
            //    scopeDetailFrame.Navigate(null);
            //}

            if (selectedEntry.ResourceMapEntry is ResourceMapItem)
            {
                ResourceMapItem resourceMapItem = (ResourceMapItem)selectedEntry.ResourceMapEntry;

                GetCandidates(resourceMapItem);

                if (Candidates.Count > 0)
                    SelectedCandidate = Candidates.First();
                else
                    PreviewContent = null;
            }
        }

        public void SelectedCandidateChanged()
        {
            if (selectedCandidate == null)
                return;

            object data = selectedCandidate.GetData();

            if (data == null)
            {
                PreviewContent = null;
                return;
            }

            byte[] byteData = null;

            object previewContent = null;

            switch (selectedCandidate.Candidate.Type)
            {
                case ResourceValueType.Path:
                case ResourceValueType.AsciiPath:
                case ResourceValueType.Utf8Path:
                    string rootPath;

                    if (selectedCandidate.Candidate.SourceFile == null)
                        rootPath = ResourceRootPath;
                    else
                        rootPath = Path.GetDirectoryName(PriFile.GetReferencedFileByRef(selectedCandidate.Candidate.SourceFile.Value).FullName);

                    string externalFilePath = Path.Combine(rootPath, (string)data);

                    if (File.Exists(externalFilePath))
                        byteData = File.ReadAllBytes(externalFilePath);
                    else
                        previewContent = new PathNotFoundPreviewPage(new PathNotFoundPreviewViewModel(externalFilePath), this);
                    break;
                case ResourceValueType.String:
                case ResourceValueType.AsciiString:
                case ResourceValueType.Utf8String:
                    previewContent = data;
                    break;
                case ResourceValueType.EmbeddedData:
                    byteData = (byte[])data;
                    break;
            }

            if (previewContent == null)
            {
                string itemName = selectedEntry?.ResourceMapEntry.Name ?? "";

                try
                {
                    if (itemName.EndsWith(".xbf", StringComparison.OrdinalIgnoreCase))
                        previewContent = new XbfPreviewPage(new XbfPreviewViewModel(byteData));
                    else if (itemName.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) ||
                        itemName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                        itemName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                        itemName.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                        itemName.EndsWith(".gif", StringComparison.OrdinalIgnoreCase) ||
                        itemName.EndsWith(".ico", StringComparison.OrdinalIgnoreCase) ||
                        itemName.EndsWith(".tif", StringComparison.OrdinalIgnoreCase) ||
                        itemName.EndsWith(".tiff", StringComparison.OrdinalIgnoreCase))
                        previewContent = new ImagePreviewPage(new ImagePreviewViewModel(byteData));
                    else if ((byteData.Length >= 3 && byteData[0] == 0xEF && byteData[1] == 0xBB && byteData[2] == 0xBF) ||
                        byteData.Length >= 2 && byteData[0] == 0xEF && byteData[1] == 0xFF ||
                        byteData.Length >= 2 && byteData[0] == 0xFF && byteData[1] == 0xEF ||
                        byteData.All(b => b >= 8 && b <= 127))
                        previewContent = new TextPreviewPage(new TextPreviewViewModel(byteData));
                    else
                        previewContent = new BinaryPreviewPage(new BinaryPreviewViewModel(byteData));
                }
                catch (Exception ex)
                {
                    TextBlock textBlock = new TextBlock();

                    textBlock.Margin = new Thickness(8);
                    textBlock.Text = ex.ToString();                    

                    previewContent = textBlock;
                }
            }

            PreviewContent = previewContent;
        }
    }
}
