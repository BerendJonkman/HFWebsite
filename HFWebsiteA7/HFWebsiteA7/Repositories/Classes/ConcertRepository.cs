using System;
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
            
            db.Events.Add(concert);
            db.SaveChanges();
        }

        public void UpdateConcert(Concert concert)
        {
            var result = GetConcert(concert.EventId);
            result.LocationId = concert.LocationId;
            result.HallId = concert.HallId;
            result.Duration = concert.Duration;
            result.StartTime = concert.StartTime;

            db.SaveChanges();
        }

        public FestivalDay CreateFestivalDay(Day day)
        {
            FestivalDay festivalDay = new FestivalDay
            {
                //De concerten zijn verdeeld over twee zalen, de main hall en een secundaire hall.
                MainConcertList = new List<Concert>(),
                SecondConcertList = new List<Concert>(),
                Day = day
            };

            List<Concert> concerts = GetConcertsByDay(day.Id);

            //Hier wordt alleen gecontrolleerd op de main hall, de rest kan in de secundaire lijst, of dat nou second of third hall is
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