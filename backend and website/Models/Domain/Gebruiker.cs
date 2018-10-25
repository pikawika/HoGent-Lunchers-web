﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.Domain
{
    public abstract class Gebruiker
    {
        [Key]
        public int GebruikerId { get; private set; }
        public string Telefoonnummer { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Email { get; set; }
        public string Wachtwoord { get; set; }
        public Rol Rol { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
    }
}