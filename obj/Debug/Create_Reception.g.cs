﻿#pragma checksum "..\..\Create_Reception.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4809F4D5D71DF50E5A294310A4892236214D3D4543071DC738701CC8AD5C47E1"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Med;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Med {
    
    
    /// <summary>
    /// Create_Reception
    /// </summary>
    public partial class Create_Reception : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\Create_Reception.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Name;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\Create_Reception.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Anamnesis;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\Create_Reception.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker Date;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\Create_Reception.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Time;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\Create_Reception.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Doctor;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\Create_Reception.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox Receptions;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\Create_Reception.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bSearchReceptions;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\Create_Reception.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bCancel;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\Create_Reception.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bCreate;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Med;component/create_reception.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Create_Reception.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Name = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.Anamnesis = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.Date = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 4:
            this.Time = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.Doctor = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.Receptions = ((System.Windows.Controls.ListBox)(target));
            return;
            case 7:
            this.bSearchReceptions = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\Create_Reception.xaml"
            this.bSearchReceptions.Click += new System.Windows.RoutedEventHandler(this.Search_Receptions);
            
            #line default
            #line hidden
            return;
            case 8:
            this.bCancel = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\Create_Reception.xaml"
            this.bCancel.Click += new System.Windows.RoutedEventHandler(this.Cancel);
            
            #line default
            #line hidden
            return;
            case 9:
            this.bCreate = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\Create_Reception.xaml"
            this.bCreate.Click += new System.Windows.RoutedEventHandler(this.Create);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
