open(html, '<', 'main.html') or die 'Cannot open file: $!';
my @temp=<html>;
$text=join "",@temp;

$cnt=0;
while($text=~ /(<table>.+?<\/table>)/){
	my $content=$1;
	my $img="<img src=\"imgt/".$cnt.".jpeg\"/>";
	$text=~ s/<table>.+?<\/table>/$img/;
	open(table, '>', 'table/'.$cnt.'.html') or die 'Cannot open file: $!';
	print table $content;
	close table;
	$cnt++;
}

open(htmlout, '>', 'main.img.html') or die 'Cannot open file: $!';
print htmlout $text;
