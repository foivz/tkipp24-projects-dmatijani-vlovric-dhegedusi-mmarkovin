# MyLibra

## Model rada na projektu

Nastavak rada na projektu iz kolegija "Razvoj programskih proizvoda"

## Opis projekta

Aplikacija MyLibra digitalizira proces posuđivanja knjiga u knjižnicama i vođenja evidencije knjiga. Aplikacija je namijenjena za članove knjižnice te zaposlenike knjižnice. Zaposlenici knjižnice imat će evidenciju nad knjigama na način da je vidljiv broj knjiga na policama, popis svih knjiga, pregled posuđenih knjiga i članova knjižnice. Članovi knjižnice koriste aplikaciju kako bi vidjeli dostupne knjige, posuđivali ih, pisali recenzije i pregledavali već postojeće. Inicijalna verzija aplikacije razvijana je u sklopu kolegija "Razvoj programskih proizvoda" koristeći .Net Framework i WPF sučelje, dislociranu bazu podataka te Visual Studio razvojno okruženje

## Projektni tim

Ime i prezime | E-mail adresa (FOI) | JMBAG | Github korisničko ime
------------  | ------------------- | ----- | ---------------------
David Matijanić | dmatijani21@student.foi.hr | 0016153844 | dmatijani
Viktor Lovrić | vlovric21@student.foi.hr | 0016154953 | vlovric21
Domagoj Hegedušić | dhegedusi21@student.foi.hr | 0016153732 | dhegedusi21
Magdalena Markovinović  | mmarkovin21@student.foi.hr | 0016155896 | mmarkoovin21

## Specifikacija projekta

Aplikacija će koristiti dislociranu bazu podataka koja sadrži podatke o knjigama, zaposlenicima, članovima knjižnice, posudbama i ostalo. Korištenje aplikacije moguće je kroz 3 uloge:
* Administrator - administratori su razvojni tim aplikacije (u sklopu ovog projekta naš tim) te korisnička podrška. Uloga administratora jedina može registrirati nove zaposlenike knjižnice na sljedeći način: kupnjom našeg softvera administratori dodaju knjižnicu i njene zaposlenike u bazu podataka. Ako postojeća knjižnica zaposli novog zaposlenika ili otpusti postojećeg, kontaktira administratore koji podešavaju stanje u bazi podataka.
* Zaposlenik knjižnice - uloga koja upravlja podacima o knjižnici tako da unosi informacije o knjigama i njihovu količinu, pregledava stanje posuđenih knjiga, posudbe i količinu. Također vide recenzije na knjige što pišu članovi knjižnice. Zaposlenici knjižnice jedini mogu registrirati nove članove tako što ti članovi moraju fizički doći u knjižnicu kako bi se izbjeglo lažno registriranje članova.
* Član knjižnice - uloga i korisnički podaci za svakog člana se dobivaju u knjižnici. Članovi mogu pregledavati sve knjige, posuđivati, rezervirati ukoliko knjiga nije na stanju, pisati recenzije, primati obavijesti i pisati zapisnik.

U aplikaciji, zaposlenici u sustavu vezani su uz određenu knjižnicu (što unosi administrator) te se sve promjene u knjižnici (nove knjige, stanje), novi članovi i njihove posudbe vežu uz tu knjižnicu (knjižnice su izolirane). Kada zaposlenik registrira novog korisnika, odnosno člana knjižnice, taj član je u sustavu automatski zabilježen pod knjižnicu zaposlenika koji ga registrira.

Oznaka | Naziv | Kratki opis | Odgovorni član tima
------ | ----- | ----------- | -------------------
F01 | Administriranje sustava | Uloga administratora moći će u komunikaciji s knjižnicom dodati ih u sustav te davati zaposlenicima knjižnice njihove korisničke podatke koje će tada oni koristiti kako bi se ulogirali kao uloga zaposlenika. Po promjeni stanja zaposlenika u knjižnici administratori imaju opciju ažuriranja podataka. | David Matijanić
F02 | Prijava i odjava | Za pristup funkcionalnostima aplikacije korisnik se prethodno mora prijaviti. Aplikacija će nuditi drugačiju vrstu pristupa ovisno o ulozi korisnika. Uloge koje se razlikuju su: administrator, zaposlenik knjižnice i član knjižnice. Svaki korisnik mora imati mogućnost odjave iz aplikacije. | Magdalena Markovinović
F03 | Upravljanje katalogom knjiga (zaposlenik) | Zaposlenici knjižnice mogu pregledavati postojeći katalog knjiga u knjižnici i unositi nove knjige i količinu. Pri unosu knjige unosi se naslov, autor, opis, godina izdavanja i ostale informacije. Knjiga se tada pohranjuje u sustav. Kada se knjiga iz bilo kojeg razloga treba maknuti iz knjižnice ona se arhivira i fizički miče sa polica te je zaposleniku dostupan popis arhiviranih knjiga sa datumima arhiviranja i tko je izvršio isto. | Viktor Lovrić
F04 | Posudba knjiga (zaposlenik) | Kada član knjižnice odluči posuditi neku knjigu ili više, zaposlenik u sustavu vidi da će biti posuđena. Kada član fizički dođe po knjigu, zaposlenik potvrđuje da je knjiga posuđena te se određuje trajanje posudbe (datum isteka se automatski izračuna). Zaposlenik knjigu može pronaći u sustavu kako bi ju zabilježio ili može skenirati barkod te knjige. Zaposlenik ima pregled svih posuđenih knjiga te informacije o posudbi (datum posudbe, član i eventualno kašnjenje). Član knjižnice ima pregled svojih prethodnih posudbi što uključuje informacije o datumu posudbe i vraćanja, knjizi. | David Matijanić
F05 | Vraćanje knjige | Kada član vrati knjigu, zaposlenik za tu posudbu bilježi da je knjiga vraćena i knjiga se vraća na stanje. To je moguće obaviti skeniranjem barkoda na knjizi. Ukoliko je član kasnio sa posudbom, pisat će broj dana koji kasni. | David Matijanić
F06 | Upravljanje članstvom (zaposlenik) | Ukoliko se osoba fizički pojavi u knjižnici i zatraži članstvo, zaposlenik tu osobu upisuje te registrira u sustav nakon čega osoba postaje član knjižnice i dobiva svoj profil s podacima koje je zaposlenik prethodno definirao. Izdaje mu se članska iskaznica sa unikatnim barkodom koja se skenira pri posuđivanju i vraćanju knjiga. Postojeći članovi mogu produžiti članstvo. Pri otvaranju novog računa ili produljivanju postojećeg izdaje se račun. Ukoliko neki član odluči prestati biti član knjižnice, knjižnica ga može ukloniti iz sustava. | Magdalena Markovinović
F07 | Pisanje i čitanje obavijesti | Na početnoj stranici aplikacije će se nalaziti panel s obavijestima (News feed) gdje će zaposlenici moći obavještavati korisnike o nabavi novih knjiga, određenim izvanrednim vijestima i slično. Za svakog korisnika se bilježi je li pročitao obavijest tako da mu se na panelu prvo prikazuju nepročitane obavijesti. | Magdalena Markovinović
F08 | Pretraživanje i filtriranje knjiga (član) | Kako bi se olakšala navigacija u aplikaciji, korisnici će moći pretraživati knjige prema imenu te filtrirati knjige prema žanru, piscu, godini, dostupnosti i ostalim svojstvima. Pritiskom na knjigu otvara se zaslon sa detaljnim informacijama o knjizi. Knjiga se može spremiti u popis „želim pročitati“ koju vidi samo taj korisnik. | Viktor Lovrić
F09 | Rezervacija knjige (član) | Ukoliko neka knjiga trenutno nije dostupna (nema je na zalihi), član knjižnice će moći rezervirati mjesto čekanja za tu knjigu. Kada se knjiga pojavi na zalihi, sustav obavještava zaposlenika knjižnice te prvog člana koji je knjigu stavio na listi čekanja kako bi ju mogao posuditi (unutar aplikacije te uživo pokupiti, F04 i F09). | Viktor Lovrić
F10 | Pisanje i pregled recenzija (član, zaposlenik) | Svaki član knjižnice će nakon vraćanja knjige moći ostaviti recenziju na tu knjigu. Pri fizičkom vraćanju knjige, zaposlenik to unosi u sustav i tom članu će biti moguće ostavljanje recenzije. Recenzija se sastoji od ocjene (1-5) i opcionalnog komentara. Zaposlenici i ostali članovi knjižnice vide sve recenzije na odabrane knjige. | Domagoj Hegedušić
F11 | Statistika poslovanja (zaposlenik) | Zaposlenik ima mogućnost pregleda cjelokupne statistike poslovanja. Statistika uključuje pregled najpopularnijih knjiga što znači da zaposlenik može vidjeti koliko je puta knjiga posuđena, koliko ukupno korisnika knjižnica ima te koji žanr ima posuđenih knjiga. | Domagoj Hegedušić
F12 | Čitanje digitaliziranih knjiga | Knjižnica omogućuje članovima knjižnice čitanje određenih digitalnih knjiga unutar aplikacije. Digitalne knjige pohranjene su u PDF formatu te se otvaraju unutar prozora aplikacije. | Domagoj Hegedušić

Nefunkcionalni zahtjevi:
- NFZ-1: Sustav će interakciju s korisnicima provoditi preko grafičkog sučelja.
- NFZ-2: Sustav će imati profesionalan izgled na strani zaposlenika i admina, a na strani člana knjižnice neće biti natrpan viškom informacija.
- NFZ-3: Sustav će biti jednostavan za korištenje zaposlenicima knjižnice i članovima.
- NFZ-4: Sustav će imati funkcionalnosti raspoređene po različitim ulogama.
- NFZ-5: Sustav će biti dostupan 24 sata na dan.
- NFZ-6: Sustav će trebati raditi na računalima sa instaliranim Windows 10 operacijskim sustavom ili novijom verzijom.
- NFZ-7: Korisničke uloge bit će izolirane tako da niti jedna uloga ne može vidjeti ono što nije toj ulozi namijenjeno.
- NFZ-8: Knjižnice će biti izolirane tako da svi korisnici vezani uz određenu knjižnicu čitaju, mijenjaju, pišu i brišu samo one podatke vezane uz tu knjižnicu. Samo admini imaju uvid u sve knjižnice u sustavu. Svi ostali korisnici imaju osjećaj kao da se u sustavu nalazi samo njihova knjižnica

## Tehnologije i oprema

Za razvoj inicijalne aplikacije koristio se .Net Framework razvojni okvir. Razvojno okruženje bilo je Visual Studio, a vrsta projekta WPF. Baza podataka je dislocirana i nalazi se na serveru. Za verzioniranje je do sada bio korišten git i GitHub te će se i dalje koristiti za nastavak razvoja projekta na kolegiju "Testiranje i kvaliteta programskih proizvoda". Dokumentacija je pisana i dodatna će biti pisana u GitHub Wiki, a projektni zadaci u GitHub Projects.
- Ova sekcija bit će ažurirana sa novim tehnologijama koje se koriste za testiranje programskih proizvoda, kako se sa njima upoznajemo na kolegiju.
