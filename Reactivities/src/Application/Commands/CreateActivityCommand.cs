﻿using System.ComponentModel.DataAnnotations;

namespace Application.Commands;
public class CreateActivityCommand : IRequest
{
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string City { get; set; }
    public string Venue { get; set; }
}

