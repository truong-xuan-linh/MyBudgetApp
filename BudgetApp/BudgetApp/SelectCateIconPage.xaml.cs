using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BudgetApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectCateIconPage : ContentPage
    {
        string type;
        string userEntry;
        string flag = "Edit";
        CategoryClass cate;
        public SelectCateIconPage()
        {
            InitializeComponent();
        }
        public SelectCateIconPage(string type_p, string Addflag, string Entry_p)
        {
            InitializeComponent();
            TransactionDatabase db = new TransactionDatabase();
            List<CateIconClass> ImageIconList =db.GetAllCateIcon();
            SelectIcon.ItemsSource = ImageIconList;
            type = type_p;
            flag = Addflag;
            userEntry = Entry_p;
        }
        public SelectCateIconPage(CategoryClass category)
        {
            InitializeComponent();
            List<CateIconClass> ImageIconList = new List<CateIconClass>();
            for (int i = 0; i < 50; i++)
            {
                string img = "icon_" + i.ToString() + ".png";
                CateIconClass cateicon = new CateIconClass();
                cateicon.IconImage = img;
                ImageIconList.Add(cateicon);
            }
            SelectIcon.ItemsSource = ImageIconList;
            
            cate = category;
        }
        private void SelectIcon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string img = (e.CurrentSelection.FirstOrDefault() as CateIconClass)?.IconImage; ;
            if (flag == "Add")
            {
                Navigation.PushAsync(new AddCategoryPage(img, type, userEntry));
            }    
            else
            {
                cate.categoryImg = img;
                Navigation.PushAsync(new EditCategoryPage(cate));
            }    
        }
    }
}