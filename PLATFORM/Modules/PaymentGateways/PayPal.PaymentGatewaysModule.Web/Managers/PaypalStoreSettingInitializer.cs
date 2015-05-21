﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtoCommerce.Domain.Store.Model;
using VirtoCommerce.Domain.Store.Services;

namespace PayPal.PaymentGatewaysModule.Web.Managers
{
	public class PaypalStoreSettingInitializer
	{
		private IStoreService _service;

		public PaypalStoreSettingInitializer(IStoreService service)
		{
			if(service == null)
				throw new ArgumentNullException("service");

			_service = service;
		}

		public void Initialize()
		{
			var stores = _service.GetStoreList();

			foreach(var store in stores)
			{
				CheckAndAddSetting("Paypal.AppKey", SettingValueType.ShortText, store);
				CheckAndAddSetting("Paypal.Secret", SettingValueType.ShortText, store);

				_service.Update(new Store[] { store });
			}
		}

		private void CheckAndAddSetting(string settingName, SettingValueType type, Store store)
		{
			var setting = store.Settings.FirstOrDefault(s => s.Name == settingName);

			if(setting == null)
			{
				store.Settings.Add(new StoreSetting
					{
						Name = settingName,
						Value = string.Empty,
						ValueType = type
					});
			}
		}
	}
}
