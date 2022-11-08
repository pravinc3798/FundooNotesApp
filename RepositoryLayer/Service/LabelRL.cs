﻿using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {
        private readonly FundooContext fundoContext;

        public LabelRL(FundooContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }

        private bool CheckUser(long userId)
        {
            var userCheck = fundoContext.UserTable.FirstOrDefault(u => u.UserId == userId);
            if (userCheck != null)
                return true;
            return false;
        }

        public LabelEntity AddLabel(long userId, string name)
        {
            try
            {
                if (CheckUser(userId))
                {
                    LabelEntity labelEntity = new LabelEntity()
                    {
                        LabelName = name,
                        UserId = userId
                    };

                    fundoContext.LabelTable.Add(labelEntity);
                    fundoContext.SaveChanges();

                    return labelEntity;
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public LabelEntity EditLabel(long userId, long labelId, string name)
        {
            try
            {
                var label = fundoContext.LabelTable.FirstOrDefault(l => l.LabelId == labelId && l.UserId == userId);

                if (label != null)
                {
                    label.LabelName = name;
                    fundoContext.SaveChanges();
                    return label;
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteLabel(long userId, long labelId)
        {
            try
            {
                var label = fundoContext.LabelTable.FirstOrDefault(l => l.LabelId == labelId && l.UserId == userId);

                if (label != null)
                {
                    fundoContext.LabelTable.Remove(label);
                    fundoContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<LabelEntity> ViewLabel(long userId)
        {
            try
            {
                var labels = fundoContext.LabelTable.Where(u => u.UserId == userId).DefaultIfEmpty();

                if (labels != null)
                {
                    return labels;
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
