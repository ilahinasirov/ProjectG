﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
	public class AutofacBusinessModule:Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<ProductManager>().As<IProductService>();
			builder.RegisterType<EfProductDal>().As<IProductDal>();

			
		}
	}
}
