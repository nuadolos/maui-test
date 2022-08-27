using System.Collections.ObjectModel;

namespace MauiTest.Models;

public class AllNotes
{
    public ObservableCollection<Note> Notes { get; set; } = new();

    public AllNotes() =>
        LoadNotes();

    public void LoadNotes()
    {
        Notes.Clear();

        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        var notes = Directory.EnumerateFiles(appDataPath, "*.notes.txt")
            .Select(filename => new Note {
                FileName = filename,
                Text = File.ReadAllText(filename),
                Date = File.GetCreationTime(filename)
            })
            .OrderBy(note => note.Date);

        foreach (var note in notes)
        {
            Notes.Add(note);
        }
    }
}
