program itage;
uses crt;
var y,n,m:integer;
x,z:real;
begin
clrscr;
write ('colichestvo etagei');
read (n);
y:=n*3;
write ('kvartira');
read (m);
if m>y then write ('crisha') else z:=m/3;
x:=trunc(x);
x:=z/2;
x:=frac(x);
if x=0 then z:=z+1;
write(z);
readkey;
end.