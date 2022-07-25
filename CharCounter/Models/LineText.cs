using Prism.Mvvm;

namespace CharCounter.Models
{
    public class LineText : BindableBase
    {
        private string text;
        private bool marked;
        private int index;
        private bool ignore;

        public string Text { get => text; set => SetProperty(ref text, value); }

        public bool Marked { get => marked; set => SetProperty(ref marked, value); }

        public int Index { get => index; set => SetProperty(ref index, value); }

        public bool Ignore { get => ignore; set => SetProperty(ref ignore, value); }
    }
}
