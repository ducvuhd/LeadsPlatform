(window.webpackJsonp=window.webpackJsonp||[]).push([[11],{"Y+KY":function(a,r,t){"use strict";t.r(r),t.d(r,"ChartJSModule",function(){return l});var c=t("LPYB"),e=t("tyNb"),o=t("fXoL");const n=[{path:"",component:(()=>{class a{constructor(){this.lineChartData=[{data:[65,59,80,81,56,55,40],label:"Series A"},{data:[28,48,40,19,86,27,90],label:"Series B"},{data:[18,48,77,9,100,27,40],label:"Series C"}],this.lineChartLabels=["January","February","March","April","May","June","July"],this.lineChartOptions={animation:!1,responsive:!0},this.lineChartColours=[{backgroundColor:"rgba(148,159,177,0.2)",borderColor:"rgba(148,159,177,1)",pointBackgroundColor:"rgba(148,159,177,1)",pointBorderColor:"#fff",pointHoverBackgroundColor:"#fff",pointHoverBorderColor:"rgba(148,159,177,0.8)"},{backgroundColor:"rgba(77,83,96,0.2)",borderColor:"rgba(77,83,96,1)",pointBackgroundColor:"rgba(77,83,96,1)",pointBorderColor:"#fff",pointHoverBackgroundColor:"#fff",pointHoverBorderColor:"rgba(77,83,96,1)"},{backgroundColor:"rgba(148,159,177,0.2)",borderColor:"rgba(148,159,177,1)",pointBackgroundColor:"rgba(148,159,177,1)",pointBorderColor:"#fff",pointHoverBackgroundColor:"#fff",pointHoverBorderColor:"rgba(148,159,177,0.8)"}],this.lineChartLegend=!0,this.lineChartType="line",this.barChartOptions={scaleShowVerticalLines:!1,responsive:!0},this.barChartLabels=["2006","2007","2008","2009","2010","2011","2012"],this.barChartType="bar",this.barChartLegend=!0,this.barChartData=[{data:[65,59,80,81,56,55,40],label:"Series A"},{data:[28,48,40,19,86,27,90],label:"Series B"}],this.doughnutChartLabels=["Download Sales","In-Store Sales","Mail-Order Sales"],this.doughnutChartData=[350,450,100],this.doughnutChartType="doughnut",this.radarChartLabels=["Eating","Drinking","Sleeping","Designing","Coding","Cycling","Running"],this.radarChartData=[{data:[65,59,90,81,56,55,40],label:"Series A"},{data:[28,48,40,19,96,27,100],label:"Series B"}],this.radarChartType="radar",this.pieChartLabels=["Download Sales","In-Store Sales","Mail Sales"],this.pieChartData=[300,500,100],this.pieChartType="pie",this.polarAreaChartLabels=["Download Sales","In-Store Sales","Mail Sales","Telesales","Corporate Sales"],this.polarAreaChartData=[300,500,100,40,120],this.polarAreaLegend=!0,this.polarAreaChartType="polarArea"}chartClicked(a){console.log(a)}chartHovered(a){console.log(a)}}return a.\u0275fac=function(r){return new(r||a)},a.\u0275cmp=o.Ub({type:a,selectors:[["ng-component"]],decls:62,vars:24,consts:[[1,"animated","fadeIn"],[1,"card-columns","cols-2"],[1,"card"],[1,"card-header"],[1,"card-header-actions"],["href","http://www.chartjs.org"],[1,"text-muted"],[1,"card-body"],[1,"chart-wrapper"],["baseChart","",1,"chart",3,"datasets","labels","options","colors","legend","chartType","chartHover","chartClick"],["baseChart","",1,"chart",3,"datasets","labels","options","legend","chartType","chartHover","chartClick"],["baseChart","",1,"chart",3,"data","labels","chartType","chartHover","chartClick"],["baseChart","",1,"chart",3,"datasets","labels","chartType","chartHover","chartClick"],["baseChart","",1,"chart",3,"data","labels","legend","chartType","chartHover","chartClick"]],template:function(a,r){1&a&&(o.gc(0,"div",0),o.gc(1,"div",1),o.gc(2,"div",2),o.gc(3,"div",3),o.Uc(4," Line Chart "),o.gc(5,"div",4),o.gc(6,"a",5),o.gc(7,"small",6),o.Uc(8,"docs"),o.fc(),o.fc(),o.fc(),o.fc(),o.gc(9,"div",7),o.gc(10,"div",8),o.gc(11,"canvas",9),o.nc("chartHover",function(a){return r.chartHovered(a)})("chartClick",function(a){return r.chartClicked(a)}),o.fc(),o.fc(),o.fc(),o.fc(),o.gc(12,"div",2),o.gc(13,"div",3),o.Uc(14," Bar Chart "),o.gc(15,"div",4),o.gc(16,"a",5),o.gc(17,"small",6),o.Uc(18,"docs"),o.fc(),o.fc(),o.fc(),o.fc(),o.gc(19,"div",7),o.gc(20,"div",8),o.gc(21,"canvas",10),o.nc("chartHover",function(a){return r.chartHovered(a)})("chartClick",function(a){return r.chartClicked(a)}),o.fc(),o.fc(),o.fc(),o.fc(),o.gc(22,"div",2),o.gc(23,"div",3),o.Uc(24," Doughnut Chart "),o.gc(25,"div",4),o.gc(26,"a",5),o.gc(27,"small",6),o.Uc(28,"docs"),o.fc(),o.fc(),o.fc(),o.fc(),o.gc(29,"div",7),o.gc(30,"div",8),o.gc(31,"canvas",11),o.nc("chartHover",function(a){return r.chartHovered(a)})("chartClick",function(a){return r.chartClicked(a)}),o.fc(),o.fc(),o.fc(),o.fc(),o.gc(32,"div",2),o.gc(33,"div",3),o.Uc(34," Radar Chart "),o.gc(35,"div",4),o.gc(36,"a",5),o.gc(37,"small",6),o.Uc(38,"docs"),o.fc(),o.fc(),o.fc(),o.fc(),o.gc(39,"div",7),o.gc(40,"div",8),o.gc(41,"canvas",12),o.nc("chartHover",function(a){return r.chartHovered(a)})("chartClick",function(a){return r.chartClicked(a)}),o.fc(),o.fc(),o.fc(),o.fc(),o.gc(42,"div",2),o.gc(43,"div",3),o.Uc(44," Pie Chart "),o.gc(45,"div",4),o.gc(46,"a",5),o.gc(47,"small",6),o.Uc(48,"docs"),o.fc(),o.fc(),o.fc(),o.fc(),o.gc(49,"div",7),o.gc(50,"div",8),o.gc(51,"canvas",11),o.nc("chartHover",function(a){return r.chartHovered(a)})("chartClick",function(a){return r.chartClicked(a)}),o.fc(),o.fc(),o.fc(),o.fc(),o.gc(52,"div",2),o.gc(53,"div",3),o.Uc(54," Polar Area Chart "),o.gc(55,"div",4),o.gc(56,"a",5),o.gc(57,"small",6),o.Uc(58,"docs"),o.fc(),o.fc(),o.fc(),o.fc(),o.gc(59,"div",7),o.gc(60,"div",8),o.gc(61,"canvas",13),o.nc("chartHover",function(a){return r.chartHovered(a)})("chartClick",function(a){return r.chartClicked(a)}),o.fc(),o.fc(),o.fc(),o.fc(),o.fc(),o.fc()),2&a&&(o.Ob(11),o.yc("datasets",r.lineChartData)("labels",r.lineChartLabels)("options",r.lineChartOptions)("colors",r.lineChartColours)("legend",r.lineChartLegend)("chartType",r.lineChartType),o.Ob(10),o.yc("datasets",r.barChartData)("labels",r.barChartLabels)("options",r.barChartOptions)("legend",r.barChartLegend)("chartType",r.barChartType),o.Ob(10),o.yc("data",r.doughnutChartData)("labels",r.doughnutChartLabels)("chartType",r.doughnutChartType),o.Ob(10),o.yc("datasets",r.radarChartData)("labels",r.radarChartLabels)("chartType",r.radarChartType),o.Ob(10),o.yc("data",r.pieChartData)("labels",r.pieChartLabels)("chartType",r.pieChartType),o.Ob(10),o.yc("data",r.polarAreaChartData)("labels",r.polarAreaChartLabels)("legend",r.polarAreaLegend)("chartType",r.polarAreaChartType))},directives:[c.a],encapsulation:2}),a})(),data:{title:"Charts"}}];let i=(()=>{class a{}return a.\u0275fac=function(r){return new(r||a)},a.\u0275mod=o.Yb({type:a}),a.\u0275inj=o.Xb({imports:[[e.g.forChild(n)],e.g]}),a})(),l=(()=>{class a{}return a.\u0275fac=function(r){return new(r||a)},a.\u0275mod=o.Yb({type:a}),a.\u0275inj=o.Xb({imports:[[i,c.b]]}),a})()}}]);