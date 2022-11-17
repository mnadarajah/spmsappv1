using SPMSCAV1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMSCAV1.SearchHandlers
{
    public class GenderSearchHandler : SearchHandler
    {
        public ObservableCollection<GenderModel> Genders { get; private set; } = new ObservableCollection<GenderModel>();

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = Genders.Where(gender => gender.Description.ToLower() == newValue.ToLower()).ToList();
            }

        }

        protected override void OnItemSelected(object item)
        {
            base.OnItemSelected(item);
        }

    }
}
