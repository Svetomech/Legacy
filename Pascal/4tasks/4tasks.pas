program triangle_equal;
uses crt;
var a,b,S,h: real;
        chs: byte;
const author: string = '������ ������';
      companyname: string = 'Svetomech Inc';
begin
   clrscr;
   writeln('��������� ������� ������ 9� ������, ', author);
   writeln(companyname, '.');
   writeln();
   writeln('����:');
   writeln('________________________________________');
   writeln('�������������� ������������� �����������');
   writeln('1. �����');
   writeln('2. ����������');
   writeln('3. ������');
   writeln('4. �������');
   writeln('________________________________________');
   writeln();
   writeln('�����: ����� �� ���� ����������');
   writeln();
   write('������� ��������� (1-4) ��������: ');
   read(chs);
   
   case chs of
   
     1:
     begin
        clrscr;
        writeln('��������� ������� ������ 9� ������, ', author);
        writeln(companyname, '.');
        writeln();
        writeln('������� � �����:');
        writeln('________________________________________');
        write('����� = '); readln(a);
        {����������}
        b:=a*sqrt(2);
        S:=(a*a)/2;
        h:=a/sqrt(2);
        {����������}
        writeln('���������� = ', b:1:2); 
        writeln('������ = ', h:1:2);
        writeln('������� = ', S:1:2);
        writeln('________________________________________');
        writeln();
     end;
     
     2:
     begin
        clrscr;
        writeln('��������� ������� ������ 9� ������, ', author);
        writeln(companyname, '.');
        writeln();
        writeln('������� � �����:');
        writeln('________________________________________');
        write('���������� = '); readln(b);
        {����������}
        a:=b/sqrt(2);
        S:=(b*b)/4;
        h:=b/2;
        {����������}
        writeln('����� = ', a:1:2);
        writeln('������ = ', h:1:2);
        writeln('������� = ', S:1:2);
        writeln('________________________________________');
        writeln();
     end;
     
     3:
     begin
        clrscr;
        writeln('��������� ������� ������ 9� ������, ', author);
        writeln(companyname, '.');
        writeln();
        writeln('������� � �����:');
        writeln('________________________________________');
        write('������ = '); readln(h);
        {����������}
        S:=(h*h);
        b:=2*h;
        a:=h*sqrt(2);
        {����������}
        writeln('����� = ', a:1:2);
        writeln('������� = ', S:1:2);
        writeln('���������� = ', b:1:2);
        writeln('________________________________________');
        writeln();
     end;
     
     4:
     begin
        clrscr;
        writeln('��������� ������� ������ 9� ������, ', author);
        writeln(companyname, '.');
        writeln();
        writeln('������� � �����:');
        writeln('________________________________________');
        write('������� = '); readln(S);
        {����������}
        a:=sqrt(2*S);
        b:=2*sqrt(S);
        h:=sqrt(S);
        {����������}
        writeln('����� = ', a:1:2);
        writeln('���������� = ', b:1:2);
        writeln('������ = ', h:1:2);
        writeln('________________________________________');
        writeln();
     end;
     
   else
     writeln();
     writeln('�����!');
     writeln();
     
   end;

readkey;
end.

