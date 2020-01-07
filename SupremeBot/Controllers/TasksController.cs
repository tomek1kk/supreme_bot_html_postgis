﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SupremeBot.Data;
using SupremeBot.Models;

namespace SupremeBot.Controllers
{
    public class TasksController : Controller
    {
        private readonly DataContext _context;

        public TasksController(DataContext context)
        {
            _context = context;
        }

        [Route("Tasks/Index")]
        [HttpGet]
        public IActionResult Index()
        {
            var items = _context.TaskItems.ToList();
            return View(items);
        }

        [Route("Tasks/Create1")]
        [HttpGet]
        public IActionResult Create1()
        {
            return View("Create1");
        }

        public IActionResult CreateTaskItem1(TaskItem task)
        {
            _context.TaskItems.Add(task);
            _context.SaveChanges();

            //return RedirectToAction("Create2");
            return RedirectToAction("Index");
        }

        [Route("Tasks/Colors")]
        [HttpGet]
        public IEnumerable<Color> Colors()
        {
            return _context.Colors.ToList();
        }

        [Route("tasks/CreateTask")]
        [HttpPost]
        public IActionResult CreateTask([FromBody] JObject data)
        {
            bool anyColor = data["anyColor"].ToObject<bool>();
            bool useTimer = data["useTimer"].ToObject<bool>();
            bool onlyWithEmptyBasket = data["onlyWithEmptyBasket"].ToObject<bool>();
            bool fillAddress = data["fillAddress"].ToObject<bool>();
            int delay = data["delay"].ToObject<int>();
            int refreshInterval = data["refreshInterval"].ToObject<int>();
            int cardId = data["card"].ToObject<int>();
            int addressId = data["address"].ToObject<int>();
            List<JObject> itemsJson = data["items"].ToObject<List<JObject>>();

            var items = new List<Item>();

            foreach (var item in itemsJson)
            {
                Categories category = item["category"].ToObject<Categories>();
                Sizes size = item["size"].ToObject<Sizes>();
                var colorsString = item["colors"].ToObject<List<string>>();
                var namesString = item["names"].ToObject<List<string>>();
                List<Color> colors = new List<Color>();
                List<ItemName> names = new List<ItemName>();

                foreach (var color in colorsString)
                {
                    var col = new Color() {Name = color};
                    colors.Add(col);
                }

                foreach (var name in namesString)
                {
                    var nam = new ItemName() { Name = name };
                    names.Add(nam);
                }

                var newItem = new Item()
                {
                    Category = category, AnyColor = true, Colors = colors,
                    Names = names, Size = size
                };

                items.Add(newItem);
            }

            Card card = _context.Cards.FirstOrDefault(x => x.Id == cardId);
            Address address = _context.Addresses.FirstOrDefault(x => x.Id == addressId);

            TaskItem task = new TaskItem()
            {
                AnyColor = anyColor, Delay = delay, FillAdress = fillAddress,
                OnlyWithEmptyBasket = onlyWithEmptyBasket, RefreshInterval = refreshInterval,
                Items = items, UseTimer = useTimer, Card = card, Address = address
            };

            _context.TaskItems.Add(task);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}