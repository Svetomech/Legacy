program house_elevator;
uses crt;
var N,fo,fa: integer;
begin
   clrscr;
   write('Кол-во этажей: ');
   readln(N);
   write('Нужная квартира: ');
   readln(fa);
   fo:=(fa-1) div 3+1;
   if (fo=N)and(fo mod 2=0) then fo:=fo-1;
   if (fo mod 2=0) then fo:=fo+1;
   if (fa>=3*N) then fo:=666;
   writeln('Ответ: вы попадете на ', fo, ' этаж');
readkey;
end.