﻿using AutoMapper;
using Calculator.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.BLL.Managers
{
    public abstract class BaseManager
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;
        public BaseManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
    }
}
