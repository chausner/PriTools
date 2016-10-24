namespace PRIExplorer.ViewModels
{
    public class PathNotFoundPreviewViewModel
    {
        public string Message { get; }

        public PathNotFoundPreviewViewModel(string path)
        {
            Message = $"Cound not find file: {path}";
        }
    }
}
