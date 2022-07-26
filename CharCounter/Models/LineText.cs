namespace CharCounter.Models
{
    using Prism.Mvvm;

    public class LineText : BindableBase
    {
        private string text;
        private bool marked;
        private int index;
        private bool ignore;
        private int counter;

        public string Text { get => text; set => SetProperty(ref text, value); }

        public bool Marked { get => marked; set => SetProperty(ref marked, value); }

        public int Index { get => index; set => SetProperty(ref index, value); }

        public bool Ignore { get => ignore; set => SetProperty(ref ignore, value); }

        public int Counter { get => counter; set => SetProperty(ref counter, value); }
    }
}
