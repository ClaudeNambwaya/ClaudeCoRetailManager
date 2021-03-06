﻿using Caliburn.Micro;
using CRMDesktopUI.Library.Api;
using CRMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
		IProductEndpoint _productEndpoint;
		public SalesViewModel(IProductEndpoint productEndpoint)
		{
			_productEndpoint = productEndpoint;
		}

		protected override async void OnViewLoaded(object view)
		{
			base.OnViewLoaded(view);
			await LoadProducts();
		}

		private async Task LoadProducts()
		{
			var ProductList = await _productEndpoint.GetAll();
			Products = new BindingList<ProductModel>(ProductList);
		}
				
		private BindingList<ProductModel> _products;

		public BindingList<ProductModel> Products
		{
			get { return _products; }
			set 
			{
				_products = value;
				NotifyOfPropertyChange(() => Products);
			}
		}

		private ProductModel _selectedProduct;

		public ProductModel SelectedProduct
		{
			get { return _selectedProduct; }
			set 
			{
				_selectedProduct = value;
				NotifyOfPropertyChange(() => SelectedProduct);
			}
		}


		private BindingList<CartItemModel> _cart = new BindingList<CartItemModel>();

		public BindingList<CartItemModel> Cart
		{
			get { return _cart; }
			set
			{
				_cart = value;
				NotifyOfPropertyChange(() => Cart);
			}
		}

		private int _itemQuantity = 1;

		public int ItemQuantity
		{
			get { return _itemQuantity; }
			set 
			{
				_itemQuantity = value;
				NotifyOfPropertyChange(() => _itemQuantity);
				NotifyOfPropertyChange(() => CanAddToCart);
			}
		}

		public string SubTotal
		{
			get 
			{
				decimal subTotal = 0;

				foreach (var item in Cart)
				{
					subTotal += (item.Product.RetailPrice* item.QuantityInCart);
				}

				return subTotal.ToString("C");
			}
		}

		public string Tax
		{
			get
			{
				// TODO - Replace with calculation
				return "Ksh0.00";
			}
		}

		public string Total
		{
			get
			{
				// TODO - Replace with calculation
				return "Ksh0.00";
			}
		}

		public bool CanAddToCart
		{
			get
			{
				bool output = false;

				// Make sure something is selected
				// Make sure there is an item quantity
				if (ItemQuantity > 0 && SelectedProduct?.QuantityinStock >= ItemQuantity)
				{
					output = true;
				}

				return output;
			}
		}
		public void AddToCart()
		{
			CartItemModel existingItem = Cart.FirstOrDefault(x => x.Product == SelectedProduct);

			if (existingItem != null)
			{
				existingItem.QuantityInCart += ItemQuantity;
				//// HACK - There should be a better way of rrefreshing the cart display
				Cart.Remove(existingItem);
				Cart.Add(existingItem);
			}
			else
			{
				CartItemModel item = new CartItemModel
				{
					Product = SelectedProduct,
					QuantityInCart = ItemQuantity
				};
				Cart.Add(item);
			}

			
			SelectedProduct.QuantityinStock -= ItemQuantity;
			ItemQuantity = 1;
			NotifyOfPropertyChange(() => SubTotal);
		}

		public bool CanRemoveFromCart
		{
			get
			{
				bool output = false;

				// Make sure something is selected

				return output;
			}
		}
		public void RemoveFromCart()
		{
			NotifyOfPropertyChange(() => SubTotal);
		}

		public bool CanCheckOut
		{
			get
			{
				bool output = false;

				// Make sure there is something in the cart

				return output;
			}
		}
		public void CheckOut()
		{

		}
	}
}
