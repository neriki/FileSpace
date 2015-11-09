/* 
 File Space
 The MIT License(MIT) 
  
 Copyright(c) 2015 Eric Boniface 
  
  Permission is hereby granted, free of charge, to any person obtaining a copy 
  of this software and associated documentation files (the "Software"), to deal 
  in the Software without restriction, including without limitation the rights 
  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
  copies of the Software, and to permit persons to whom the Software is 
  furnished to do so, subject to the following conditions: 
  
 The above copyright notice and this permission notice shall be included in 
  all copies or substantial portions of the Software. 
  
 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
  THE SOFTWARE. 
  
 */

using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Collections.Generic;

namespace FileSpace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        private ClassScan _oScan;
        private bool _showDup;
        private GridViewColumnHeader _orderColumnHeader;

        public MainWindow()
        {
            _showDup = false;
            InitializeComponent();
        }

        public void ClearList()
        {
            ListFile.Items.Clear();
        }

        public void AddLine(ClassFsItem itm)
        {
            ListFile.Items.Add(itm);
        }


        private void BtnBrowse_click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                TextPath.Text = dialog.FileName;
            }

        }

        private void BtnScan_click(object sender, RoutedEventArgs e)
        {
            if (_showDup)
            {
                _showDup = false;
                BtnScanDup.Content = "Search duplicate";
            }

            _oScan = new ClassScan();

            _oScan.Scan(TextPath.Text, this);

            if (_oScan.ErrorList != "")
                MessageBox.Show(_oScan.ErrorList);
        }

        private void BtnOpen_click(object sender, RoutedEventArgs e)
        {
            OpenDir();
        }

        private void BtnDelete_click(object sender, RoutedEventArgs e)
        {

             
            if (ListFile.SelectedItem != null)
            {
                if (ListFile.SelectedItems.Count == 1)
                {
                    if (MessageBox.Show(((ClassFsItem)ListFile.SelectedItem).deleteText,
                                "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        _oScan.deleteFile(((ClassFsItem)ListFile.SelectedItem), this);
                    }
                }
                else
                {
                    ClassFsItem[] selected=new ClassFsItem[ListFile.SelectedItems.Count];

                    ListFile.SelectedItems.CopyTo(selected,0);

                    if (MessageBox.Show("Do you want to delete all the selected file.",
                                   "Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        foreach (ClassFsItem itm in selected)
                        {
                            _oScan.deleteFile(itm, this);
                        }
                    }
                    
                   
                }

            }
        }

        private void BtnScanSel_click(object sender, RoutedEventArgs e)
        {

            if (ListFile.SelectedItem != null)
            {
                if (((ClassFsItem)ListFile.SelectedItem).Type == "Dir.")
                {
                    if (_showDup)
                    {
                        _showDup = false;
                        BtnScanDup.Content = "Search duplicate";
                    }

                    TextPath.Text = ((ClassFsItem)ListFile.SelectedItem).Name;

                    _oScan = new ClassScan();

                    _oScan.Scan(TextPath.Text, this);

                    if (_oScan.ErrorList != "")
                        MessageBox.Show(_oScan.ErrorList);
                }
            }
        }


        private void BtnScanDup_click(object sender, RoutedEventArgs e)
        {
            if (_showDup)
            {
                _showDup = false;
                _oScan.Show(this);
                reorder();
                BtnScanDup.Content = "Search duplicate";
            }
            else
            {
                if (ListFile.SelectedItem != null)
                {
                    if (((ClassFsItem)ListFile.SelectedItem).Type == "File")
                    {
                        _showDup = true;
                        _oScan.ShowDuplicate(((ClassFile)ListFile.SelectedItem).Md5(), this);
                        BtnScanDup.Content = "Show all";
                    }
                }
            }
        }

        private void TextPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                _oScan = new ClassScan();

                _oScan.Scan(TextPath.Text, this);

                if (_oScan.ErrorList != "")
                    MessageBox.Show(_oScan.ErrorList);
            }
        }

        private void ListFileHeaderClick(object sender, RoutedEventArgs e)
        {

            _orderColumnHeader = e.OriginalSource as GridViewColumnHeader;
            reorder();
        }

        private void reorder()
        {
            if (_orderColumnHeader != null && _orderColumnHeader.Column != null)
            {

                Binding b = _orderColumnHeader.Column.DisplayMemberBinding as Binding;

                if (b != null)
                {
                    ICollectionView resultDataView = CollectionViewSource.GetDefaultView(
                        ListFile.Items);

                    ListSortDirection sortDirection = ListSortDirection.Ascending;
                    string columnName = b.Path.PathParameters[0].ToString().Split(' ')[1];

                    if (resultDataView.SortDescriptions.Count > 0
                            && resultDataView.SortDescriptions[0].PropertyName == columnName
                            && resultDataView.SortDescriptions[0].Direction == ListSortDirection.Ascending)
                        sortDirection = ListSortDirection.Descending;

                    resultDataView.SortDescriptions.Clear();
                    resultDataView.SortDescriptions.Add(
                        new SortDescription(columnName, sortDirection));


                }
            }
        }


        private void ListFile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //OpenDir();
        }

        private void OpenDir()
        {
            if (ListFile.SelectedItem != null)
            {
                ((ClassFsItem)ListFile.SelectedItem).OpenInExplorer();
            }
        }
    }
}
