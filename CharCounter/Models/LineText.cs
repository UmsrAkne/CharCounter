using Prism.Mvvm;

namespace CharCounter.Models
{
    public class LineText : BindableBase
    {
        private string text;
        private bool marked;

        public string Text { get => text; set => SetProperty(ref text, value); }
        public bool Marked { get => marked; set => SetProperty(ref marked, value); }
    }
}
