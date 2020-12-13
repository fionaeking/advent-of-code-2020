using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day4
{
    public class Validation
    {
        public bool ValidateFields(Dictionary<string, string> fields)
        {
            var passport = CreatePassportObject(fields);
            return ValidateYear(passport.byr, 1920, 2002)
            && ValidateYear(passport.iyr, 2010, 2020)
            && ValidateYear(passport.eyr, 2020, 2030)
            && ValidateHeight(passport.hgt)
            && ValidateHairColour(passport.hcl)
            && ValidateEyeColour(passport.ecl)
            && ValidatePassportId(passport.pid);
        }

        private bool ValidateYear(string yearString, int minYear, int maxYear)
        {
            return int.TryParse(yearString, out var year) && (minYear <= year && year <= maxYear);
        }

        private bool ValidatePassportId(string pid)
        {
            // pid (Passport ID) - a nine-digit number, including leading zeroes.
            var rgx = new Regex(@"^[0-9]{9}$");
            return rgx.IsMatch(pid);
        }

        private bool ValidateEyeColour(string ecl)
        {
            // ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
            var validEyeColours = new List<string>() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            return validEyeColours.Contains(ecl);
        }

        private bool ValidateHairColour(string hcl)
        {
            // hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
            var rgx = new Regex(@"#([0-9]|[a-f]){6}");
            return rgx.IsMatch(hcl);
        }

        private bool ValidateHeight(string hgt)
        {
            //hgt(Height) - a number followed by either cm or in:
            //If cm, the number must be at least 150 and at most 193.
            //If in, the number must be at least 59 and at most 76.
            var unit = hgt.Substring(Math.Max(0, hgt.Length - 2));
            var success = int.TryParse(hgt.Remove(Math.Max(0, hgt.Length - 2)), out var value);
            if (!success)
            {
                return false;
            }
            else return ((unit == "cm" && 150 <= value && value <= 193)
                | (unit == "in" && 59 <= value && value <= 76));
        }

        private Passport CreatePassportObject(Dictionary<string, string> fields)
        {
            return new Passport()
            {
                byr = fields["byr"],
                ecl = fields["ecl"],
                eyr = fields["eyr"],
                hcl = fields["hcl"],
                hgt = fields["hgt"],
                iyr = fields["iyr"],
                pid = fields["pid"]
            };
        }

    }

    public class Passport
    {
        public string ecl;
        public string pid;
        public string eyr;
        public string hcl;
        public string byr;
        public string iyr;
        public string hgt;
    }
}