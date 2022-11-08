using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL labelRL;

        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        public LabelEntity AddLabel(long userId, string name)
        {
            try
            {
                return labelRL.AddLabel(userId, name);
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
                return labelRL.EditLabel(userId, labelId, name);
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
                return labelRL.DeleteLabel(userId, labelId);
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
                return labelRL.ViewLabel(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
