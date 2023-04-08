using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DynamicMenu.Models;

namespace DynamicMenu.Services
{
    public class UserService
    {
        private static readonly List<User> validUsers = new List<User>
        {
            new User
            {
                Name = "Archeologie",
                Prava = new List<MenuButtonRight> { MenuButtonRight.Archeologie }
            },
            new User
            {
                Name = "Bozp",
                Prava = new List<MenuButtonRight> { MenuButtonRight.BOZP }
            },
            new User
            {
                Name = "MonitoringBozp",
                Prava = new List<MenuButtonRight> { MenuButtonRight.Monitoring, MenuButtonRight.BOZP }
            },
            new User
            {
                Name = "ArcheologieMonitoring",
                Prava = new List<MenuButtonRight> { MenuButtonRight.Archeologie, MenuButtonRight.Monitoring }
            },
            new User
            {
                Name = "All",
                Prava = new List<MenuButtonRight> { MenuButtonRight.Archeologie, MenuButtonRight.Monitoring, MenuButtonRight.BOZP }
            }
        };

        /// <summary>
        /// <c>null</c> if not found
        /// </summary>
        public User GetUserByName(string name)
        {
            var user = (from item in validUsers where item.Name == name select item).SingleOrDefault();
            return user;
        }
    }
}
