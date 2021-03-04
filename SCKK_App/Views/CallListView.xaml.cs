﻿using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.CompilerServices;
using SCKK_App.Controllers;
using SCKK_App.Requests;
using SCKK_App.Models;

namespace SCKK_App.Views
{
    /// <summary>
    /// Interaction logic for CallList.xaml
    /// </summary>
    public partial class CallListView
    {
        private bool sidebarOpen = false;
        private bool isGrouped = false;
        List<ResultModel> log;

        public CallListView(List<ResultModel> log)
        {
            InitializeComponent();
            DataGridStatistic.ItemsSource = log;
            this.log = log;
        }

        private void BarOpener_Click(object sender, RoutedEventArgs e)
        {
            GridLengthConverter gridLengthConverter = new GridLengthConverter();

            if (sidebarOpen)
            {
                ButtonBar.Height = (GridLength)gridLengthConverter.ConvertFrom("0");
                BarOpenerIcon.Kind = PackIconKind.ArrowDropUp;

                sidebarOpen = false;
            }
            else
            {
                ButtonBar.Height = (GridLength)gridLengthConverter.ConvertFrom("60");

                BarOpenerIcon.Kind = PackIconKind.ArrowDropDown;
                sidebarOpen = true;
            }
        }

        private void Grouping_Click(object sender, RoutedEventArgs e)
        {
            if (isGrouped)
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(DataGridStatistic.ItemsSource);
                view.GroupDescriptions.Clear();

                GrouperBtn.Content = "Csoportosítás";

                isGrouped = false;
            }
            else
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(DataGridStatistic.ItemsSource);
                view.GroupDescriptions.Add(new PropertyGroupDescription("name"));

                GrouperBtn.Content = "Alap rendezés";

                isGrouped = true;
            }
        }

        private void UploadBtn_Click(object sender, RoutedEventArgs e)
        {
            var UploadWindow = new UploadView(log);
            UploadWindow.Show();
        }
    }
}