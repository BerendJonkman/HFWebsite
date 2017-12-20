﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HFWebsiteA7.Models;
using HFWebsiteA7.Repositories.Interfaces;

namespace HFWebsiteA7.Repositories.Classes
{
    public class ConcertRepository : IConcertsRepository
    {
        private HFWebsiteA7Context db = new HFWebsiteA7Context();

        public void AddConcert(Concert concert)
        {
            db.Concerts.Add(concert);
            db.SaveChanges();
        }

        public FestivalDay CreateFestivalDay(Day day)
        {
            FestivalDay festivalDay = new FestivalDay
            {
                MainConcertList = new List<Concert>(),
                SecondConcertList = new List<Concert>(),
                Day = day
            };

            List<Concert> concerts = GetConcertsByDay(day.Id);

            foreach (Concert concert in concerts)
            {
                if (concert.Hall.Name.Equals("Main Hall"))
                {
                    festivalDay.MainConcertList.Add(concert);
                }
                else
                {
                    festivalDay.SecondConcertList.Add(concert);
                }
            }
            return festivalDay;
        }

        public IEnumerable<Concert> GetAllConcerts()
        {
            return db.Concerts.ToList();
        }

        public Concert GetConcert(int concertId)
        {
            return db.Concerts.Find(concertId);
        }

        public List<Concert> GetConcertsByDay(int dayId)
        {
            List<Concert> allConcerts = db.Concerts.ToList();
            List<Concert> dayConcerts = new List<Concert>();

            foreach (Concert concert in allConcerts)
            {
                if (concert.Day.Id == dayId)
                {
                    dayConcerts.Add(concert);
                }
            }

            return dayConcerts;
        }
    }
}