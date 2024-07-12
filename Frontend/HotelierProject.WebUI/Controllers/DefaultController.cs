﻿using HotelierProject.WebUI.Dtos.SubscribeDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace HotelierProject.WebUI.Controllers;

[AllowAnonymous]
public class DefaultController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public DefaultController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public PartialViewResult _SubscribePartial()
    {
        return PartialView();
    }
    [HttpPost]
    public async Task<IActionResult> _SubscribePartial(CreateSubscribeDto createSubscribeDto)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        string jsonData = JsonConvert.SerializeObject(createSubscribeDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        await client.PostAsync("http://localhost:5160/api/Subscribe", stringContent);
        return RedirectToAction("Index", "Default");
    }
}

