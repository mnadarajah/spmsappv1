using SPMSCAV1.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SPMSCAV1.ViewModels
{
    public class SchedulerViewModel : BaseViewModel
    {
        public SchedulerViewModel()
        {
            Title = "Scheduler";
            Items = new ObservableCollection<Item>();
        }

        public ObservableCollection<Item> Items { get; private set; }

        async public void OnAppearing()
        {
            IEnumerable<Item> items = await DataStore.GetItemsAsync(true);
            Items.Clear();
            foreach (Item item in items)
            {
                Items.Add(item);
            }
        }
    }
}