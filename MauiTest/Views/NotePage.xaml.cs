using MauiTest.Models;

namespace MauiTest.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage
{
	public string ItemId { set => LoadNote(value); }

	public NotePage()
	{
		InitializeComponent();

		var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		var randomFileName = $"{Path.GetRandomFileName()}.notes.txt";

		LoadNote(Path.Combine(appDataPath, randomFileName));
	}

	private async void SaveButton_Clicked(object sender, EventArgs e)
	{
		if (BindingContext is Note note)
			File.WriteAllText(note.FileName, TextEditor.Text);

		await Shell.Current.GoToAsync("..");
    }

	private async void DeleteButton_Clicked(object sender, EventArgs e)
	{
		if (BindingContext is Note note)
		{
			if (File.Exists(note.FileName))
				File.Delete(note.FileName);
		}

		await Shell.Current.GoToAsync("..");
	}

	private void LoadNote(string fileName)
	{
		var note = new Note {
			FileName = fileName
		};

		if (File.Exists(fileName))
		{
			note.Date = File.GetCreationTime(fileName);
			note.Text = File.ReadAllText(fileName);
		}

		BindingContext = note;
	}
}