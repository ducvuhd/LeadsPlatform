!function(){function c(c,e){if(!(c instanceof e))throw new TypeError("Cannot call a class as a function")}function e(c,e){for(var o=0;o<e.length;o++){var t=e[o];t.enumerable=t.enumerable||!1,t.configurable=!0,"value"in t&&(t.writable=!0),Object.defineProperty(c,t.key,t)}}(window.webpackJsonp=window.webpackJsonp||[]).push([[14],{AgMk:function(o,t,d){"use strict";d.r(t),d.d(t,"ThemeModule",function(){return m});var g,n,i,a,r=d("ofXK"),f=d("tyNb"),l=d("NuRj"),s=d("fXoL"),p=[{path:"",data:{title:"Theme"},children:[{path:"",redirectTo:"colors"},{path:"colors",component:(n=function(){function o(e){c(this,o),this._document=e}var t,d,g;return t=o,(d=[{key:"themeColors",value:function(){var c=this;Array.from(this._document.querySelectorAll(".theme-color")).forEach(function(e){var o=Object(l.getStyle)("background-color",e),t=c._document.createElement("table");t.innerHTML='\n        <table class="w-100">\n          <tr>\n            <td class="text-muted">HEX:</td>\n            <td class="font-weight-bold">'.concat(Object(l.rgbToHex)(o),'</td>\n          </tr>\n          <tr>\n            <td class="text-muted">RGB:</td>\n            <td class="font-weight-bold">').concat(o,"</td>\n          </tr>\n        </table>\n      "),e.parentNode.appendChild(t)})}},{key:"ngOnInit",value:function(){this.themeColors()}}])&&e(t.prototype,d),g&&e(t,g),o}(),n.\u0275fac=function(c){return new(c||n)(s.ac(r.d))},n.\u0275cmp=s.Ub({type:n,selectors:[["ng-component"]],decls:131,vars:0,consts:[[1,"animated","fadeIn"],[1,"card"],[1,"card-header"],[1,"icon-drop"],[1,"card-body"],[1,"row"],[1,"col-xl-2","col-md-3","col-sm-4","col-6","mb-4"],[1,"bg-primary","theme-color","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-secondary","theme-color","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-success","theme-color","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-danger","theme-color","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-warning","theme-color","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-info","theme-color","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-light","theme-color","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-dark","theme-color","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"row","mb-3"],[1,"bg-gray-100","theme-color","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-gray-200","theme-color","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-gray-300","theme-color","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-gray-400","theme-color","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-gray-500","theme-color","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-gray-600","theme-color","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-gray-700","theme-color","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-gray-800","theme-color","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-gray-900","theme-color","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-blue","theme-color","mb-3","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-light-blue","theme-color","mb-3","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-indigo","theme-color","mb-3","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-purple","theme-color","mb-3","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-pink","theme-color","mb-3","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-red","theme-color","mb-3","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-orange","theme-color","mb-3","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-yellow","theme-color","mb-3","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-green","theme-color","mb-3","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-teal","theme-color","mb-3","w-75","rounded","mb-2",2,"padding-top","75%"],[1,"bg-cyan","theme-color","mb-3","w-75","rounded","mb-2",2,"padding-top","75%"]],template:function(c,e){1&c&&(s.gc(0,"div",0),s.gc(1,"div",1),s.gc(2,"div",2),s.bc(3,"i",3),s.Uc(4," Theme colors "),s.fc(),s.gc(5,"div",4),s.gc(6,"div",5),s.gc(7,"div",6),s.bc(8,"div",7),s.gc(9,"h6"),s.Uc(10,"Brand Primary Color"),s.fc(),s.fc(),s.gc(11,"div",6),s.bc(12,"div",8),s.gc(13,"h6"),s.Uc(14,"Brand Secondary Color"),s.fc(),s.fc(),s.gc(15,"div",6),s.bc(16,"div",9),s.gc(17,"h6"),s.Uc(18,"Brand Success Color"),s.fc(),s.fc(),s.gc(19,"div",6),s.bc(20,"div",10),s.gc(21,"h6"),s.Uc(22,"Brand Danger Color"),s.fc(),s.fc(),s.gc(23,"div",6),s.bc(24,"div",11),s.gc(25,"h6"),s.Uc(26,"Brand Warning Color"),s.fc(),s.fc(),s.gc(27,"div",6),s.bc(28,"div",12),s.gc(29,"h6"),s.Uc(30,"Brand Info Color"),s.fc(),s.fc(),s.gc(31,"div",6),s.bc(32,"div",13),s.gc(33,"h6"),s.Uc(34,"Brand Light Color"),s.fc(),s.fc(),s.gc(35,"div",6),s.bc(36,"div",14),s.gc(37,"h6"),s.Uc(38,"Brand Dark Color"),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.gc(39,"div",1),s.gc(40,"div",2),s.bc(41,"i",3),s.Uc(42," Grays "),s.fc(),s.gc(43,"div",4),s.gc(44,"div",15),s.gc(45,"div",6),s.bc(46,"div",16),s.gc(47,"h6"),s.Uc(48,"Gray 100 Color"),s.fc(),s.fc(),s.gc(49,"div",6),s.bc(50,"div",17),s.gc(51,"h6"),s.Uc(52,"Gray 200 Color"),s.fc(),s.fc(),s.gc(53,"div",6),s.bc(54,"div",18),s.gc(55,"h6"),s.Uc(56,"Gray 300 Color"),s.fc(),s.fc(),s.gc(57,"div",6),s.bc(58,"div",19),s.gc(59,"h6"),s.Uc(60,"Gray 400 Color"),s.fc(),s.fc(),s.gc(61,"div",6),s.bc(62,"div",20),s.gc(63,"h6"),s.Uc(64,"Gray 500 Color"),s.fc(),s.fc(),s.gc(65,"div",6),s.bc(66,"div",21),s.gc(67,"h6"),s.Uc(68,"Gray 600 Color"),s.fc(),s.fc(),s.gc(69,"div",6),s.bc(70,"div",22),s.gc(71,"h6"),s.Uc(72,"Gray 700 Color"),s.fc(),s.fc(),s.gc(73,"div",6),s.bc(74,"div",23),s.gc(75,"h6"),s.Uc(76,"Gray 800 Color"),s.fc(),s.fc(),s.gc(77,"div",6),s.bc(78,"div",24),s.gc(79,"h6"),s.Uc(80,"Gray 900 Color"),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.gc(81,"div",1),s.gc(82,"div",2),s.bc(83,"i",3),s.Uc(84," Additional colors "),s.fc(),s.gc(85,"div",4),s.gc(86,"div",5),s.gc(87,"div",6),s.bc(88,"div",25),s.gc(89,"h6"),s.Uc(90,"Blue Color"),s.fc(),s.fc(),s.gc(91,"div",6),s.bc(92,"div",26),s.gc(93,"h6"),s.Uc(94,"Light Blue Color"),s.fc(),s.fc(),s.gc(95,"div",6),s.bc(96,"div",27),s.gc(97,"h6"),s.Uc(98,"Indigo Color"),s.fc(),s.fc(),s.gc(99,"div",6),s.bc(100,"div",28),s.gc(101,"h6"),s.Uc(102,"Purple Color"),s.fc(),s.fc(),s.gc(103,"div",6),s.bc(104,"div",29),s.gc(105,"h6"),s.Uc(106,"Pink Color"),s.fc(),s.fc(),s.gc(107,"div",6),s.bc(108,"div",30),s.gc(109,"h6"),s.Uc(110,"Red Color"),s.fc(),s.fc(),s.gc(111,"div",6),s.bc(112,"div",31),s.gc(113,"h6"),s.Uc(114,"Orange Color"),s.fc(),s.fc(),s.gc(115,"div",6),s.bc(116,"div",32),s.gc(117,"h6"),s.Uc(118,"Yellow Color"),s.fc(),s.fc(),s.gc(119,"div",6),s.bc(120,"div",33),s.gc(121,"h6"),s.Uc(122,"Green Color"),s.fc(),s.fc(),s.gc(123,"div",6),s.bc(124,"div",34),s.gc(125,"h6"),s.Uc(126,"Teal Color"),s.fc(),s.fc(),s.gc(127,"div",6),s.bc(128,"div",35),s.gc(129,"h6"),s.Uc(130,"Cyan Color"),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.fc())},encapsulation:2}),n),data:{title:"Colors"}},{path:"typography",component:(g=function e(){c(this,e)},g.\u0275fac=function(c){return new(c||g)},g.\u0275cmp=s.Ub({type:g,selectors:[["ng-component"]],decls:189,vars:0,consts:[[1,"animated","fadeIn"],[1,"card"],[1,"card-header"],[1,"card-body"],[1,"table"],[1,"highlighter-rouge"],[1,"h1"],[1,"h2"],[1,"h3"],[1,"h4"],[1,"h5"],[1,"h6"],[1,"bd-example"],[1,"bd-example","bd-example-type"],[1,"display-1"],[1,"display-2"],[1,"display-3"],[1,"display-4"],[1,"row"],[1,"col-sm-3"],[1,"col-sm-9"],[1,"col-sm-3","text-truncate"],[1,"col-sm-4"],[1,"col-sm-8"]],template:function(c,e){1&c&&(s.gc(0,"div",0),s.gc(1,"div",1),s.gc(2,"div",2),s.Uc(3," Headings "),s.fc(),s.gc(4,"div",3),s.gc(5,"p"),s.Uc(6,"Documentation and examples for Bootstrap typography, including global settings, headings, body text, lists, and more."),s.fc(),s.gc(7,"table",4),s.gc(8,"thead"),s.gc(9,"tr"),s.gc(10,"th"),s.Uc(11,"Heading"),s.fc(),s.gc(12,"th"),s.Uc(13,"Example"),s.fc(),s.fc(),s.fc(),s.gc(14,"tbody"),s.gc(15,"tr"),s.gc(16,"td"),s.gc(17,"p"),s.gc(18,"code",5),s.Uc(19,"<h1></h1>"),s.fc(),s.fc(),s.fc(),s.gc(20,"td"),s.gc(21,"span",6),s.Uc(22,"h1. Bootstrap heading"),s.fc(),s.fc(),s.fc(),s.gc(23,"tr"),s.gc(24,"td"),s.gc(25,"p"),s.gc(26,"code",5),s.Uc(27,"<h2></h2>"),s.fc(),s.fc(),s.fc(),s.gc(28,"td"),s.gc(29,"span",7),s.Uc(30,"h2. Bootstrap heading"),s.fc(),s.fc(),s.fc(),s.gc(31,"tr"),s.gc(32,"td"),s.gc(33,"p"),s.gc(34,"code",5),s.Uc(35,"<h3></h3>"),s.fc(),s.fc(),s.fc(),s.gc(36,"td"),s.gc(37,"span",8),s.Uc(38,"h3. Bootstrap heading"),s.fc(),s.fc(),s.fc(),s.gc(39,"tr"),s.gc(40,"td"),s.gc(41,"p"),s.gc(42,"code",5),s.Uc(43,"<h4></h4>"),s.fc(),s.fc(),s.fc(),s.gc(44,"td"),s.gc(45,"span",9),s.Uc(46,"h4. Bootstrap heading"),s.fc(),s.fc(),s.fc(),s.gc(47,"tr"),s.gc(48,"td"),s.gc(49,"p"),s.gc(50,"code",5),s.Uc(51,"<h5></h5>"),s.fc(),s.fc(),s.fc(),s.gc(52,"td"),s.gc(53,"span",10),s.Uc(54,"h5. Bootstrap heading"),s.fc(),s.fc(),s.fc(),s.gc(55,"tr"),s.gc(56,"td"),s.gc(57,"p"),s.gc(58,"code",5),s.Uc(59,"<h6></h6>"),s.fc(),s.fc(),s.fc(),s.gc(60,"td"),s.gc(61,"span",11),s.Uc(62,"h6. Bootstrap heading"),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.gc(63,"div",1),s.gc(64,"div",2),s.Uc(65," Headings "),s.fc(),s.gc(66,"div",3),s.gc(67,"p"),s.gc(68,"code",5),s.Uc(69,".h1"),s.fc(),s.Uc(70," through "),s.gc(71,"code",5),s.Uc(72,".h6"),s.fc(),s.Uc(73," classes are also available, for when you want to match the font styling of a heading but cannot use the associated HTML element."),s.fc(),s.gc(74,"div",12),s.gc(75,"p",6),s.Uc(76,"h1. Bootstrap heading"),s.fc(),s.gc(77,"p",7),s.Uc(78,"h2. Bootstrap heading"),s.fc(),s.gc(79,"p",8),s.Uc(80,"h3. Bootstrap heading"),s.fc(),s.gc(81,"p",9),s.Uc(82,"h4. Bootstrap heading"),s.fc(),s.gc(83,"p",10),s.Uc(84,"h5. Bootstrap heading"),s.fc(),s.gc(85,"p",11),s.Uc(86,"h6. Bootstrap heading"),s.fc(),s.fc(),s.fc(),s.fc(),s.gc(87,"div",1),s.gc(88,"div",2),s.Uc(89," Display headings "),s.fc(),s.gc(90,"div",3),s.gc(91,"p"),s.Uc(92,"Traditional heading elements are designed to work best in the meat of your page content. When you need a heading to stand out, consider using a "),s.gc(93,"strong"),s.Uc(94,"display heading"),s.fc(),s.Uc(95,"\u2014a larger, slightly more opinionated heading style."),s.fc(),s.gc(96,"div",13),s.gc(97,"table",4),s.gc(98,"tbody"),s.gc(99,"tr"),s.gc(100,"td"),s.gc(101,"span",14),s.Uc(102,"Display 1"),s.fc(),s.fc(),s.fc(),s.gc(103,"tr"),s.gc(104,"td"),s.gc(105,"span",15),s.Uc(106,"Display 2"),s.fc(),s.fc(),s.fc(),s.gc(107,"tr"),s.gc(108,"td"),s.gc(109,"span",16),s.Uc(110,"Display 3"),s.fc(),s.fc(),s.fc(),s.gc(111,"tr"),s.gc(112,"td"),s.gc(113,"span",17),s.Uc(114,"Display 4"),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.gc(115,"div",1),s.gc(116,"div",2),s.Uc(117," Inline text elements "),s.fc(),s.gc(118,"div",3),s.gc(119,"p"),s.Uc(120,"Traditional heading elements are designed to work best in the meat of your page content. When you need a heading to stand out, consider using a "),s.gc(121,"strong"),s.Uc(122,"display heading"),s.fc(),s.Uc(123,"\u2014a larger, slightly more opinionated heading style."),s.fc(),s.gc(124,"div",12),s.gc(125,"p"),s.Uc(126,"You can use the mark tag to "),s.gc(127,"mark"),s.Uc(128,"highlight"),s.fc(),s.Uc(129," text."),s.fc(),s.gc(130,"p"),s.gc(131,"del"),s.Uc(132,"This line of text is meant to be treated as deleted text."),s.fc(),s.fc(),s.gc(133,"p"),s.gc(134,"s"),s.Uc(135,"This line of text is meant to be treated as no longer accurate."),s.fc(),s.fc(),s.gc(136,"p"),s.gc(137,"ins"),s.Uc(138,"This line of text is meant to be treated as an addition to the document."),s.fc(),s.fc(),s.gc(139,"p"),s.gc(140,"u"),s.Uc(141,"This line of text will render as underlined"),s.fc(),s.fc(),s.gc(142,"p"),s.gc(143,"small"),s.Uc(144,"This line of text is meant to be treated as fine print."),s.fc(),s.fc(),s.gc(145,"p"),s.gc(146,"strong"),s.Uc(147,"This line rendered as bold text."),s.fc(),s.fc(),s.gc(148,"p"),s.gc(149,"em"),s.Uc(150,"This line rendered as italicized text."),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.gc(151,"div",1),s.gc(152,"div",2),s.Uc(153," Description list alignment "),s.fc(),s.gc(154,"div",3),s.gc(155,"p"),s.Uc(156,"Align terms and descriptions horizontally by using our grid system\u2019s predefined classes (or semantic mixins). For longer terms, you can optionally add a "),s.gc(157,"code",5),s.Uc(158,".text-truncate"),s.fc(),s.Uc(159," class to truncate the text with an ellipsis."),s.fc(),s.gc(160,"div",12),s.gc(161,"dl",18),s.gc(162,"dt",19),s.Uc(163,"Description lists"),s.fc(),s.gc(164,"dd",20),s.Uc(165,"A description list is perfect for defining terms."),s.fc(),s.gc(166,"dt",19),s.Uc(167,"Euismod"),s.fc(),s.gc(168,"dd",20),s.gc(169,"p"),s.Uc(170,"Vestibulum id ligula porta felis euismod semper eget lacinia odio sem nec elit."),s.fc(),s.gc(171,"p"),s.Uc(172,"Donec id elit non mi porta gravida at eget metus."),s.fc(),s.fc(),s.gc(173,"dt",19),s.Uc(174,"Malesuada porta"),s.fc(),s.gc(175,"dd",20),s.Uc(176,"Etiam porta sem malesuada magna mollis euismod."),s.fc(),s.gc(177,"dt",21),s.Uc(178,"Truncated term is truncated"),s.fc(),s.gc(179,"dd",20),s.Uc(180,"Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus."),s.fc(),s.gc(181,"dt",19),s.Uc(182,"Nesting"),s.fc(),s.gc(183,"dd",20),s.gc(184,"dl",18),s.gc(185,"dt",22),s.Uc(186,"Nested definition list"),s.fc(),s.gc(187,"dd",23),s.Uc(188,"Aenean posuere, tortor sed cursus feugiat, nunc augue blandit nunc."),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.fc())},encapsulation:2}),g),data:{title:"Typography"}}]}],h=((a=function e(){c(this,e)}).\u0275fac=function(c){return new(c||a)},a.\u0275mod=s.Yb({type:a}),a.\u0275inj=s.Xb({imports:[[f.g.forChild(p)],f.g]}),a),m=((i=function e(){c(this,e)}).\u0275fac=function(c){return new(c||i)},i.\u0275mod=s.Yb({type:i}),i.\u0275inj=s.Xb({imports:[[r.c,h]]}),i)}}])}();