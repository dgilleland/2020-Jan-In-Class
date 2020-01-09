using BackEnd.DataModels;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BLL
{
    public class BlackMarketController
    {
        public List<Vehicle> GetVehicles()
        {
            var carFaker = new Faker<Vehicle>()
                .StrictMode(true)
                .RuleFor(c => c.VIN, faker => faker.Vehicle.Vin())
                .RuleFor(c => c.Manufacturer, faker => faker.Vehicle.Manufacturer())
                .RuleFor(c => c.Model, faker => faker.Vehicle.Model())
                .RuleFor(c => c.Type, faker => faker.Vehicle.Type())
                .RuleFor(c => c.Fuel, faker => faker.Vehicle.Fuel());
            var result = carFaker.Generate(10);
            return result;
        }
        public Member GetMemberAds(int memberId)
        {
            var faker = new Faker();
            var info = new Member
            {
                MemberId = memberId,
                Name = faker.Name.FullName(),
                PostedAds = new List<Ad>()
            };
            int adId = 1, itemId = 200;
            string email = faker.Internet.Email();
            var adFaker = new Faker<Ad>()
                //.StrictMode(true)
                .RuleFor(a => a.AdId, f => adId++)
                .RuleFor(a => a.PostingDate, f => f.Date.Between(DateTime.Now.AddMonths(-1), DateTime.Now))
                .RuleFor(a => a.Description, f => $"For Sale: {f.Hacker.Phrase()}")//.Commerce.ProductName()}")
                .RuleFor(a => a.EmailContact, f => email)
                .RuleFor(a => a.Title, f => "Limited Time Offer");
            var itemFaker = new Faker<Item>()
                .RuleFor(i => i.ItemId, f => itemId++)
                .RuleFor(i => i.Name, f => f.Commerce.ProductName())
                .RuleFor(i => i.Quantity, f => f.Random.Int(min: 0, max: 4))
                .RuleFor(i => i.TotalItemPrice, f => f.Finance.Amount(25, 500));
            var ads = adFaker.Generate(5);
            foreach(var ad in ads)
            {
                ad.ClosedDate = ad.PostingDate.AddDays(faker.Random.Int(min: 0, max: 4));
                ad.ClosedDate = ad.ClosedDate.Value > DateTime.Now.AddDays(-14) ? null : ad.ClosedDate;
                var items = itemFaker.Generate(faker.Random.Int(min: 1, max: 5));
                foreach(var item in items)
                {
                    item.Sold = ad.ClosedDate.HasValue;
                }
                ad.Items = items;
            }

            info.PostedAds = ads.OrderBy(x => x.PostingDate).ToList();
            return info;
        }
    }
}
