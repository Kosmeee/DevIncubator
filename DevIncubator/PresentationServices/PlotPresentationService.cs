using DevIncubator.DbContext;
using DevIncubator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevIncubator.PresentationServices
{
    public class PlotPresentationService
    {

        public void AddData(UserData data)
        {
            for (var i = data.Start;i<=data.End;i+=data.Step)
            {
                data.Points.Add(
                    new Point
                    {
                        PointX = i,
                        PointY = data.A * i * i + data.B * i + data.C

                    }); 
            }
            using (var db = new MyDbContext())
            {
                db.UserDatas.Add(data);
                db.SaveChanges();
            }
        }
    }
       
}
