using Lunchers.Models.Domain;
using System;
using System.Collections.Generic;

namespace Lunchers.Models.Repositories
{
    public interface IGebruikerRepository
    {
        Gebruiker Login(string gebruikersnaam, string hash);

        Boolean EmailExists(string email);

        Boolean GebruikersnaamExists(string gebruikersnaam);

        void Registreer(Gebruiker gebruiker);

        void WijzigWachtwoord(int gebruikersId, byte[] nieuweSalt, string nieuweHash);

        byte[] getSalt(string gebruikersnaam);
    }
}
 