program itage;
uses crt;
var y,n,m:integer;
x,z:real;
begin
   clrscr;
   write('Кол-во этажей: ');
   readln(n);
   write('Квартира: ');
   read(m);
   y:=n*3;
   if m>y then writeln('Крыша же!') else z:=m div 3;
   x:=z/2;
   x:=frac(x);
   if (x=0) then z:=z+1;
   if (x=0)and(z=n) then z:=z-1;
   writeln(z);
readkey;
end.