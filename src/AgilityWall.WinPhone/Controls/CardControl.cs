﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace AgilityWall.WinPhone.Controls
{
    public class CardControl : Control
    {
        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
            "Image", typeof (Uri), typeof (CardControl), new PropertyMetadata(default(Uri)));

        public Uri Image
        {
            get { return (Uri) GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof (string), typeof (CardControl), new PropertyMetadata(default(string)));

        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty HasDescriptionProperty = DependencyProperty.Register(
            "HasDescription", typeof (bool), typeof (CardControl), new PropertyMetadata(default(bool)));

        public bool HasDescription
        {
            get { return (bool) GetValue(HasDescriptionProperty); }
            set { SetValue(HasDescriptionProperty, value); }
        }

        public static readonly DependencyProperty HasCommentsProperty = DependencyProperty.Register(
            "HasComments", typeof (bool), typeof (CardControl), new PropertyMetadata(default(bool)));

        public bool HasComments
        {
            get { return (bool) GetValue(HasCommentsProperty); }
            set { SetValue(HasCommentsProperty, value); }
        }

        public static readonly DependencyProperty CommentsProperty = DependencyProperty.Register(
            "Comments", typeof (int), typeof (CardControl), new PropertyMetadata(default(int)));

        public int Comments
        {
            get { return (int) GetValue(CommentsProperty); }
            set { SetValue(CommentsProperty, value); }
        }

        public static readonly DependencyProperty HasListsProperty = DependencyProperty.Register(
            "HasLists", typeof (bool), typeof (CardControl), new PropertyMetadata(default(bool)));

        public bool HasLists
        {
            get { return (bool) GetValue(HasListsProperty); }
            set { SetValue(HasListsProperty, value); }
        }

        public static readonly DependencyProperty ListItemsCompleteProperty = DependencyProperty.Register(
            "ListItemsComplete", typeof (int), typeof (CardControl), new PropertyMetadata(default(int)));

        public int ListItemsComplete
        {
            get { return (int) GetValue(ListItemsCompleteProperty); }
            set { SetValue(ListItemsCompleteProperty, value); }
        }

        public static readonly DependencyProperty TotalListsProperty = DependencyProperty.Register(
            "TotalLists", typeof (int), typeof (CardControl), new PropertyMetadata(default(int)));

        public int TotalLists
        {
            get { return (int) GetValue(TotalListsProperty); }
            set { SetValue(TotalListsProperty, value); }
        }

        public static readonly DependencyProperty HasAttachmentsProperty = DependencyProperty.Register(
            "HasAttachments", typeof (bool), typeof (CardControl), new PropertyMetadata(default(bool)));

        public bool HasAttachments
        {
            get { return (bool) GetValue(HasAttachmentsProperty); }
            set { SetValue(HasAttachmentsProperty, value); }
        }

        public static readonly DependencyProperty AttatchmentsProperty = DependencyProperty.Register(
            "Attatchments", typeof (int), typeof (CardControl), new PropertyMetadata(default(int)));

        public int Attatchments
        {
            get { return (int) GetValue(AttatchmentsProperty); }
            set { SetValue(AttatchmentsProperty, value); }
        }

        public static readonly DependencyProperty DueDateProperty = DependencyProperty.Register(
            "DueDate", typeof (string), typeof (CardControl), new PropertyMetadata(default(string)));

        public string DueDate
        {
            get { return (string) GetValue(DueDateProperty); }
            set { SetValue(DueDateProperty, value); }
        }
    }
}