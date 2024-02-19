using Google.Cloud.Translation.V2;
using System;
using System.Windows.Forms;

namespace LanguageTranslationTool
{
    public partial class MainForm : Form
    {
        private TranslationClient translationClient;

        public MainForm()
        {
            InitializeComponent();
            InitializeTranslationClient();
        }

        private void InitializeTranslationClient()
        {
            try
            {
                translationClient = TranslationClient.CreateFromApiKey("YOUR_API_KEY");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing translation client: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void TranslateButton_Click(object sender, EventArgs e)
        {
            if (translationClient == null)
            {
                MessageBox.Show("Translation client is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string textToTranslate = inputTextBox.Text;
                string targetLanguage = targetLanguageComboBox.SelectedItem.ToString();
                TranslationResult result = await translationClient.TranslateTextAsync(textToTranslate, targetLanguage);
                outputTextBox.Text = result.TranslatedText;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error translating text: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
