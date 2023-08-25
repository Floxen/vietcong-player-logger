# VCLogger

Vítejte v programu, který umožňuje připojit hráče do hry a uvítá je.
Aplikace je konkrétně pro hru "Vietcong 1"

## Popis

Tento program slouží k připojení a uvítání hráčů. Po spuštění programu mají hráči možnost se připojit do hry a budou srdečně uvítáni následujícím způsobem (např): "Welcome jmeno hrace (lokace hrace)", kde `{name}` je jméno připojeného hráče a `{country}` je informace o státu nebo původu hráče.

## Použití

1. Nejprve si ověřte, že máte nainstalované nezbytné závislosti a prostředí pro spuštění programu.

2. Spusťte program.

3. Hráč bude po připojení na server uvítán zprávou ve formátu "Welcome jmeno hrace (lokace hrace)" (tu můžete upravit v souboru vclogger.ini).

4. V konfiguračním souboru to bude vypadat následovně : Welcome {name} ({country})

## Důležité info

1. Aplikace nemá definovanou **working directory**, to znamená že je třeba mít aplikaci umístěnou ve složce s **vietcong** serverem!

