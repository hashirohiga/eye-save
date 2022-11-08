using Eye_Save_New.Entities;
using Eye_Save_New.Entities.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye_Save_New.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private List<Agent> _agents;
        private List<Agent> _displayingAgents;
        private string _searchValue;
        private List<string> _filterList = new();

        //поле и свойство для введенного значения
        private string _filterValue;

        public string FilterValue
        {
            get { return _filterValue; }
            set
            {
                Set(ref _filterValue, value, nameof(FilterValue));
                DisplayAgents();
            }
        }

        public List<string> FilterList
        {
            get { return _filterList; }
            set
            {
                Set(ref _filterList, value, nameof(FilterList));
            }
        }
        //инциализация и заполнения сортировочного листа
        public List<string> SortList => new()
        {
            "Без сортировки",
            "По названию (возр.)",
            "По названию (уб.)",
            "По скидке (возр.)",
            "По скидке (уб.)",
            "По приоритету (возр.)",
            "По приоритету (уб.)"
        };
        //поле и свойство для введенного значения
        private string _sortValue;

        public string SortValue
        {
            get { return _sortValue; }
            set
            {
                Set(ref _sortValue, value, nameof(SortValue));
                DisplayAgents();
            }
        }

        //поле и свойство для введенного значения
        public string SearchValue
        {
            get { return _searchValue; }
            set
            {
                Set(ref _searchValue, value, nameof(SearchValue));
                DisplayAgents();
            }
        }
        
        public List<Agent> DisplayingAgents
        {
            get { return _displayingAgents; }
            set => Set(ref _displayingAgents, value, nameof(DisplayingAgents));
        }
        //конструктор
        public MainWindowViewModel()
        {


            using (ApplicationDbContext context = new())
            {
                FilterList.Add("Все типы");
                var List = context.AgentTypes.ToList();
                List.ForEach(a => FilterList.Add(a.Title));

                _agents = context.Agents
                    .Include(a => a.AgentType)
                    .Include(ps => ps.ProductSales)
                    .ThenInclude(p => p.Product)
                    .ToList();
            }
            DisplayingAgents = new List<Agent>(_agents);
            SortValue = SortList[0];
            FilterValue = FilterList[0];
        }



        private void DisplayAgents()
        {
            //порядок выполнения методов
            DisplayingAgents = Sorting(Search(Filtering(_agents)));
        }
        //метод фильтрации
        private List<Agent> Filtering(List<Agent> agents)
        {
            if (FilterValue == FilterList[0])
            {
                return agents;
            }
            else
            {
                return agents.Where(a => a.AgentType.Title == FilterValue).ToList();
            }
        }
        //метод сортировка
        private List<Agent> Sorting(List<Agent> agents)
        {
            if (SortValue == SortList[1])
            {
                return agents.OrderBy(a => a.Title).ToList();
            }
            else if (SortValue == SortList[2])
            {
                return agents.OrderByDescending(a => a.Title).ToList();
            }
            else if (SortValue == SortList[3])
            {
                return agents.OrderBy(a => a.Discount).ToList();
            }
            else if (SortValue == SortList[4])
            {
                return agents.OrderByDescending(a => a.Discount).ToList();
            }
            else if (SortValue == SortList[5])
            {
                return agents.OrderBy(a => a.Priority).ToList();
            }
            if (SortValue == SortList[6])
            {
                return agents.OrderByDescending(a => a.Priority).ToList();
            }
            else
            {
                return agents;
            }
        }
        //метод поиск
        private List<Agent> Search(List<Agent> agents)
        {
            if (SearchValue == null || SearchValue == String.Empty)
                return agents;
            else
            {
                return agents.Where(a => a.Title.ToLower().Contains(SearchValue.ToLower())
                || a.Email.ToLower().Contains(SearchValue.ToLower())
                || a.Phone.ToLower().Contains(SearchValue.ToLower())).ToList();
            }
        }

    }
}
