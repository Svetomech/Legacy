program predictor_like;
uses crt;
var year: word;
begin
   clrscr;
   writeln('�������� ��������');
   writeln();
   write('���: ');
   read(year);
   year:=year mod 12;
   
   case year of
   
     0:
     begin
        clrscr;
        writeln('�������� ��������');
        writeln();
        write('��������');
        writeln();
     end;
     
     1:
     begin
        clrscr;
        writeln('�������� ��������');
        writeln();
        write('�����');
        writeln();
     end;
     
     2:
     begin
        clrscr;
        writeln('�������� ��������');
        writeln();
        write('������');
        writeln();
     end;
     
     3:
     begin
        clrscr;
        writeln('�������� ��������');
        writeln();
        write('�����');
        writeln();
     end;
     
     4:
     begin
        clrscr;
        writeln('�������� ��������');
        writeln();
        write('�����');
        writeln();
     end;
     
     5:
     begin
        clrscr;
        writeln('�������� ��������');
        writeln();
        write('���');
        writeln();
     end;
     
     6:
     begin
        clrscr;
        writeln('�������� ��������');
        writeln();
        write('����');
        writeln();
     end;
     
     7:
     begin
        clrscr;
        writeln('�������� ��������');
        writeln();
        write('����');
        writeln();
     end;
     
     8:
     begin
        clrscr;
        writeln('�������� ��������');
        writeln();
        write('������');
        writeln();
     end;
     
     9:
     begin
        clrscr;
        writeln('�������� ��������');
        writeln();
        write('����');
        writeln();
     end;
     
     10:
     begin
        clrscr;
        writeln('�������� ��������');
        writeln();
        write('������');
        writeln();
     end;
     
     11:
     begin
        clrscr;
        writeln('�������� ��������');
        writeln();
        write('����');
        writeln();
     end;
     
   else
     writeln();
     writeln('IMPOSSIBRU!');
     writeln();
     
   end;

readkey;
end.

