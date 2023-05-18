﻿using Data.Entities;
using Repositories.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.MenuItemOffers
{
    public interface IMenuItemOffersRepository : IRepository<MenuItemOffer>
    {
        bool RemoveMenuItemOffersByOfferId(int offerId);
    }
}
