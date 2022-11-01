﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Фасхиева_ПР6
{
    public partial class Group
    {
        public SolidColorBrush ColorCost
        {
            get
            {
                SeasonTicket ticket = DataBase.bd.SeasonTicket.FirstOrDefault(x => x.idGroup == idGroup);
                if (ticket.cost >= 10000)
                {
                    return Brushes.Aqua;
                }
                else
                {
                    return Brushes.White;
                }
            }
        }
    }
}
