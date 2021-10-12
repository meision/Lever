using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meision.VisualStudio.CustomCommands
{
    public enum CultureDisplayMode
    {
        LCID = 0,
        Name = 1,
        WindowsName = 2,
        EnglishName = 3,
        NativeName = 4,
    }

    public sealed class LanguageManager
    {
        #region Static
        //public static int CompareLCID(int lcid1, int lcid2)
        //{
        //    return NeutralCultureManager.Default._lcidSequence[lcid1].CompareTo(NeutralCultureManager.Default._lcidSequence[lcid2]);
        //}
        public static CultureInfo InvariantCulture
        {
            get
            {
                return CultureInfo.InvariantCulture;
            }
        }

        public static int CompareByEnglishName(CultureInfo culture1, CultureInfo culture2)
        {
            return culture1.EnglishName.CompareTo(culture2.EnglishName);
        }

        public static int CompareByName(CultureInfo culture1, CultureInfo culture2)
        {
            return culture1.Name.CompareTo(culture2.Name);
        }

        public static string GetCultureCaption(CultureInfo culture, CultureDisplayMode mode)
        {
            switch (mode)
            {
                case CultureDisplayMode.LCID:
                    return culture.LCID.ToString();
                case CultureDisplayMode.Name:
                    return culture.Name;
                case CultureDisplayMode.WindowsName:
                    return culture.ThreeLetterWindowsLanguageName;
                case CultureDisplayMode.EnglishName:
                    return culture.EnglishName;
                case CultureDisplayMode.NativeName:
                    return culture.NativeName;
                default:
                    return culture.LCID.ToString();
            }
        }
        #endregion Static

        #region Field & Property
        private ReadOnlyCollection<CultureInfo> _cultures;
        public ReadOnlyCollection<CultureInfo> Cultures
        {
            get
            {
                return this._cultures;
            }
        }
        #endregion Field & Property

        #region Constructor
        private static LanguageManager __instance = new LanguageManager();
        public static LanguageManager Default
        {
            get
            {
                return __instance;
            }
        }

        private LanguageManager()
        {
            string text =
@"ar
bg
ca
zh-Hans
cs
da
de
el
en
es
fi
fr
he
hu
is
it
ja
ko
nl
no
pl
pt
rm
ro
ru
hr
sk
sq
sv
th
tr
ur
id
uk
be
sl
et
lv
lt
tg
fa
vi
hy
az
eu
hsb
mk
tn
xh
zu
af
ka
fo
hi
mt
se
ga
ms
kk
ky
sw
tk
uz
tt
bn
pa
gu
or
ta
te
kn
ml
as
mr
sa
mn
bo
cy
km
lo
gl
kok
syr
si
iu
am
tzm
ne
fy
ps
fil
dv
ha
yo
quz
nso
ba
lb
kl
ig
ii
arn
moh
br

ug
mi
oc
co
gsw
sah
qut
rw
wo
prs
gd
bs-Cyrl
bs-Latn
sr-Cyrl
sr-Latn
smn
az-Cyrl
sms
zh
nn
bs
az-Latn
sma
uz-Cyrl
mn-Cyrl
iu-Cans
zh-Hant
nb
sr
tg-Cyrl
dsb
smj
uz-Latn
mn-Mong
iu-Latn
tzm-Latn
ha-Latn";
            // Get all cultures.
            string[] lines = text.Split(new string[] { "\n" }, StringSplitOptions.None);
            CultureInfo[] cultures = new CultureInfo[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                cultures[i] = new CultureInfo(lines[i].Trim());
            }
            Array.Sort(cultures, CompareByName);
            this._cultures = new ReadOnlyCollection<CultureInfo>(cultures);
        }
        #endregion Constructor

        #region Method
        public CultureInfo FindCulture(string key, CultureDisplayMode mode)
        {
            switch (mode)
            {
                case CultureDisplayMode.LCID:
                    return this.GetCultureById(System.Convert.ToInt32(key));
                case CultureDisplayMode.Name:
                    return this.GetCultureByName(key);
                case CultureDisplayMode.WindowsName:
                    return this.GetCultureByWindowsName(key);
                case CultureDisplayMode.EnglishName:
                    return this.GetCultureByEnglishName(key);
                case CultureDisplayMode.NativeName:
                    return this.GetCultureByNativeName(key);
                default:
                    return null;
            }
        }

        public CultureInfo GetCultureById(int lcid)
        {
            return this._cultures.FirstOrDefault(p => p.LCID == lcid);
        }
        public CultureInfo GetCultureByName(string name)
        {
            return this._cultures.FirstOrDefault(p => p.Name == name);
        }
        public CultureInfo GetCultureByWindowsName(string windowsName)
        {
            return this._cultures.FirstOrDefault(p => p.ThreeLetterWindowsLanguageName == windowsName);
        }
        public CultureInfo GetCultureByEnglishName(string name)
        {
            return this._cultures.FirstOrDefault(p => p.EnglishName == name);
        }
        public CultureInfo GetCultureByNativeName(string name)
        {
            return this._cultures.FirstOrDefault(p => p.NativeName == name);
        }

        public CultureInfo GetCurrentNeutralCulture()
        {
            System.Globalization.CultureInfo culture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            return GetNeutralCulture(culture);
        }
        public CultureInfo GetNeutralCulture(int lcid)
        {
            return GetNeutralCulture(new CultureInfo(lcid));
        }
        public CultureInfo GetNeutralCulture(CultureInfo culture)
        {
            CultureInfo current = culture;
            while (!current.IsNeutralCulture)
            {
                if (current == System.Globalization.CultureInfo.InvariantCulture)
                {
                    return current;
                }

                current = current.Parent;
            }
            return this.GetCultureById(current.LCID);
        }
        #endregion Method
    }
}
