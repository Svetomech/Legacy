program house_elevator;
uses crt;
var N,fo,fa: integer;
begin
   clrscr;
   write('���-�� ������: ');
   readln(N);
   write('������ ��������: ');
   readln(fa);
   fo:=(fa-1) div 3+1;
   if (fo=N)and(fo mod 2=0) then fo:=fo-1;
   if (fo mod 2=0) then fo:=fo+1;
   if (fa>=3*N) then fo:=666;
   writeln('�����: �� �������� �� ', fo, ' ����');
readkey;
end.