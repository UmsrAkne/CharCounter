namespace CharCounter.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Controls;
    using CharCounter.Models;
    using Prism.Commands;
    using Prism.Mvvm;

    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Application";
        private ObservableCollection<LineText> texts = new ObservableCollection<LineText>();
        private LineText selectedItem;
        private int selectedLineIndex;
        private bool ignoreFileIsVisible = true;
        private int ignoreFileCount;
        private int maximumIndex;
        private int markedFileCount;
        private string searchString;

        public MainWindowViewModel()
        {
        }

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public ObservableCollection<LineText> Texts
        {
            get => texts;
            set => SetProperty(ref texts, value);
        }

        public LineText SelectedItem { get => selectedItem; set => SetProperty(ref selectedItem, value); }

        public int SelectedLineIndex { get => selectedLineIndex; set => SetProperty(ref selectedLineIndex, value); }

        public bool IgnoreFileIsVisible { get => ignoreFileIsVisible; set => SetProperty(ref ignoreFileIsVisible, value); }

        public int IgnoreFileCount { get => ignoreFileCount; set => SetProperty(ref ignoreFileCount, value); }

        public int MaximumIndex { get => maximumIndex; set => SetProperty(ref maximumIndex, value); }

        public int MarkedFileCount { get => markedFileCount; set => SetProperty(ref markedFileCount, value); }

        public string SearchString { get => searchString; set => SetProperty(ref searchString, value); }

        public int ListViewItemLineHeight => 15;

        public DelegateCommand<ListView> CursorUpCommand => new DelegateCommand<ListView>(lv =>
        {
            if (SelectedLineIndex > 0)
            {
                SelectedLineIndex--;
                lv.ScrollIntoView(SelectedItem);
            }
        });

        public DelegateCommand<ListView> CursorDownCommand => new DelegateCommand<ListView>(lv =>
        {
            if (SelectedLineIndex < Texts.Count - 1)
            {
                SelectedLineIndex++;
                lv.ScrollIntoView(SelectedItem);
            }
        });

        public DelegateCommand<ListView> CursorPageUpCommand => new DelegateCommand<ListView>((lv) =>
        {
            var command = CursorUpCommand;
            Enumerable.Range(0, GetDisplayingItemCount(lv)).ToList().ForEach(i => command.Execute(lv));
        });

        public DelegateCommand<ListView> CursorPageDownCommand => new DelegateCommand<ListView>((lv) =>
        {
            var command = CursorDownCommand;
            Enumerable.Range(0, GetDisplayingItemCount(lv)).ToList().ForEach(i => command.Execute(lv));
        });

        public DelegateCommand ToggleIgnoreFileCommand => new DelegateCommand(() =>
        {
            // if (SelectedItem != null)
            // {
            //     SelectedItem.Ignore = !SelectedItem.Ignore;
            //     IgnoreFileCount += SelectedItem.Ignore ? 1 : -1;
            //
            //     if (!IgnoreFileIsVisible)
            //     {
            //         var index = SelectedLineIndex; // Remove を行うとインデックスがリセットされるため変数に保持する。
            //         Texts.Remove(SelectedItem);
            //         SelectedLineIndex = index;
            //     }
            // }

            // ReIndex();
        });

        public DelegateCommand ToggleMarkCommand => new DelegateCommand(() =>
        {
            if (SelectedItem != null)
            {
                SelectedItem.Marked = !SelectedItem.Marked;
                MarkedFileCount += SelectedItem.Marked ? 1 : -1;
            }
        });

        public DelegateCommand<ListView> JumpToNextMarkCommand => new DelegateCommand<ListView>((lv) =>
        {
            var nextMark = Texts.Skip(SelectedLineIndex + 1).FirstOrDefault(f => f.Marked);

            if (nextMark != null)
            {
                lv.ScrollIntoView(nextMark);
                SelectedLineIndex = Texts.IndexOf(nextMark);
            }
        });

        public DelegateCommand<ListView> JumpToPrevMarkCommand => new DelegateCommand<ListView>((lv) =>
        {
            var prevMark = Texts.Take(SelectedLineIndex - 1).Reverse().FirstOrDefault(f => f.Marked);

            if (prevMark != null)
            {
                lv.ScrollIntoView(prevMark);
                SelectedLineIndex = Texts.IndexOf(prevMark);
            }
        });

        public DelegateCommand DisplayIgnoreFileCommand => new DelegateCommand(() =>
        {
            IgnoreFileIsVisible = true;
            ReloadCommand.Execute();
        });

        public DelegateCommand HideIgnoreFileCommand => new DelegateCommand(() =>
        {
            IgnoreFileIsVisible = false;
            ReloadCommand.Execute();
        });

        public DelegateCommand AppendPrefixToIgnoreFilesCommand => new DelegateCommand(() =>
        {
            // doubleFileList.AppendPrefixToIgnoreFiles("ignore");
            ReloadCommand.Execute();
        });

        public DelegateCommand AppendNumberCommand => new DelegateCommand(() =>
        {
            Texts.ToList().ForEach(t => t.Text = $"{t.Counter.ToString("0000")},{t.Text}");

            // doubleFileList.AppendNumber();
            ReloadCommand.Execute();
        });

        public DelegateCommand AppendNumberWithoutIgnoreFileCommand => new DelegateCommand(() =>
        {
            // doubleFileList.AppendNumberWithoutIgnoreFile();
            ReloadCommand.Execute();
        });

        public DelegateCommand ReloadCommand => new DelegateCommand(() =>
        {
            if (IgnoreFileIsVisible)
            {
                // ExtendFileInfos = new ObservableCollection<ExtendFileInfo>(doubleFileList.GetFiles());
            }
            else
            {
                // ExtendFileInfos = new ObservableCollection<ExtendFileInfo>(doubleFileList.GetExceptedIgnoreFiles());
            }
        });

        public DelegateCommand CharCountCommand => new DelegateCommand(() =>
        {
            var counter = 0;
            foreach (var text in Texts)
            {
                if (text.Text.Contains(SearchString))
                {
                    text.Counter = ++counter;
                }
                else
                {
                    text.Counter = 0;
                }
            }
        });

        // 基本的にビヘイビアから呼び出される
        public void SetTextFile(List<LineText> lines)
        {
            Texts = new ObservableCollection<LineText>(lines);

            MarkedFileCount = 0;
            IgnoreFileCount = 0;

            ReIndex();
        }

        private void ReIndex()
        {
            var index = 1;

            foreach (var f in Texts)
            {
                f.Index = index++;
            }

            MaximumIndex = index - 1;
        }

        private int GetDisplayingItemCount(ListView lv)
        {
            // + 5 はボーダー等によるズレの補正値。厳密に正確な表示数が出るわけではない。大体当たっている程度。
            return (int)Math.Floor(lv.ActualHeight / (ListViewItemLineHeight + 5));
        }
    }
}
