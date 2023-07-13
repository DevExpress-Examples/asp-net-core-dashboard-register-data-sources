using DevExpress.Xpo;

namespace WebDashboardDataSources {
    [Persistent("Categories"), DeferredDeletion(false)]
    public class Category: XPCustomObject {
        int categoryId;
        string categoryName;
        string description;
        [Key]
        public int CategoryID {
            get { return categoryId; }
            set { SetPropertyValue<int>("CategoryID", ref categoryId, value); }
        }
        public string CategoryName {
            get { return categoryName; }
            set { SetPropertyValue<string>("CategoryName", ref categoryName, value); }
        }
        public string Description {
            get { return description; }
            set { SetPropertyValue<string>("Description", ref description, value); }
        }
    }
}
