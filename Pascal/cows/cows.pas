program cows_ponies;
uses crt;
var b,k,t: integer;
const author: string = '������ ������';
      companyname: string = 'Svetomech Inc';
begin
   clrscr;
   writeln('��������� ������� ������ 9� ������, ', author);
   writeln(companyname, '.');
   writeln();
   writeln('����:');
   writeln('______________________________________________');
   writeln('��������� 1-�� ����: 10 �.�.');
   writeln('��������� 1-�� ������: 5 �.�.');
   writeln('��������� 1-�� �������: 0.5 �.�.');
   writeln();
   writeln('�� 100 �.�. ���������� ������ 100 ����� �����.');
   writeln('������� ��� ��������.');
   writeln('______________________________________________');
   writeln();
   writeln('�����:');
   writeln('_______________________________________________');
   for b:=0 to 10 do
   for k:=0 to 20 do
   for t:=0 to 200 do
   begin
   if ((20*b)+(10*k)+t=200)and(b+k+t=100) then writeln(b, ' ���(-��), ', k, ' �����(-�), ', t, ' �����(-����)');
   end;
   writeln('_______________________________________________');
   writeln();
readkey;
end.

