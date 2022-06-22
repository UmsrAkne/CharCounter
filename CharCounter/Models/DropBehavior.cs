namespace CharCounter.Models
{
    using System.Windows;
    using Microsoft.Xaml.Behaviors;

    public class DropBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            // ファイルをドラッグしてきて、コントロール上に乗せた際の処理
            AssociatedObject.PreviewDragOver += this.AssociatedObject_PreviewDragOver;

            // ファイルをドロップした際の処理
            AssociatedObject.Drop += this.AssociatedObject_Drop;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewDragOver -= this.AssociatedObject_PreviewDragOver;
            AssociatedObject.Drop -= this.AssociatedObject_Drop;
        }

        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            // ファイルパスの一覧の配列
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            // int index = 0;
            // var fileList = files.OrderBy(p => p).Select(p => new ExtendFileInfo(p) { Index = ++index }).ToList();

            // ((sender as Window).DataContext as MainWindowViewModel).SetFiles(fileList);
        }

        private void AssociatedObject_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            e.Handled = e.Data.GetDataPresent(DataFormats.FileDrop);
        }
    }
}
