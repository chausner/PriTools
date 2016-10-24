using PriFormat;
using System.Collections.ObjectModel;

namespace PRIExplorer.ViewModels
{
    public class EntryViewModel
    {
        public ResourceMapEntry ResourceMapEntry { get; }
        public EntryType Type { get; }
        public string Icon { get; set; }
        public bool IsString { get; set; }

        public ObservableCollection<EntryViewModel> Children { get; }

        public EntryViewModel(ResourceMapEntry resourceMapEntry)
        {
            ResourceMapEntry = resourceMapEntry;
            Type = resourceMapEntry is ResourceMapScope ? EntryType.Scope : EntryType.Item;

            Children = new ObservableCollection<EntryViewModel>();
        }

        public string Name
        {
            get
            {
                return ResourceMapEntry.Name;
            }
        }
    }

    public class StringEntryViewModel : EntryViewModel
    {
        public StringEntryViewModel(ResourceMapEntry resourceMapEntry, string name) : base(resourceMapEntry)
        {
            Name = name;
            Icon = "/Assets/blue-document-attribute-s.png";
        }

        public new string Name { get; }
    }

    public enum EntryType
    {
        Scope,
        Item
    }
}
