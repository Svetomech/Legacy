program cows_ponies;
uses crt;
var b,k,t: integer;
const author: string = 'Беляев Андрей';
      companyname: string = 'Svetomech Inc';
begin
   clrscr;
   writeln('Программу написал ученик 9А класса, ', author);
   writeln(companyname, '.');
   writeln();
   writeln('Дано:');
   writeln('______________________________________________');
   writeln('Стоимость 1-го быка: 10 у.е.');
   writeln('Стоимость 1-ой коровы: 5 у.е.');
   writeln('Стоимость 1-го теленка: 0.5 у.е.');
   writeln();
   writeln('На 100 у.е. необходимо купить 100 голов скота.');
   writeln('Указать все вариации.');
   writeln('______________________________________________');
   writeln();
   writeln('Ответ:');
   writeln('_______________________________________________');
   for b:=0 to 10 do
   for k:=0 to 20 do
   for t:=0 to 200 do
   begin
   if ((20*b)+(10*k)+t=200)and(b+k+t=100) then writeln(b, ' бык(-ов), ', k, ' коров(-ы), ', t, ' телят(-енка)');
   end;
   writeln('_______________________________________________');
   writeln();
readkey;
end.

