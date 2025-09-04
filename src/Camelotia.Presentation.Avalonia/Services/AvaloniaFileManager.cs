using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Camelotia.Services.Interfaces;

namespace Camelotia.Presentation.Avalonia.Services;

public sealed class AvaloniaFileManager : IFileManager
{
    private readonly Window _window;

    public AvaloniaFileManager(Window window) => _window = window;

    public async Task<Stream> OpenWrite(string name)
    {
        // Use Window.StorageProvider for Avalonia 11
        var result = await _window.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            Title = "Select folder"
        });

        var folder = result.FirstOrDefault();
        if (folder is null)
            throw new OperationCanceledException("No folder selected.");

        // Create a new file inside the selected folder
        var file = await folder.CreateFileAsync(name);
        return await file.OpenWriteAsync();
    }

    public async Task<(string Name, Stream Stream)> OpenRead()
    {
        var result = await _window.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Select file",
            AllowMultiple = false
        });

        var file = result.FirstOrDefault();
        if (file is null)
            throw new OperationCanceledException("No file selected.");

        var name = file.Name;
        var stream = await file.OpenReadAsync();
        return (name, stream);
    }
}
