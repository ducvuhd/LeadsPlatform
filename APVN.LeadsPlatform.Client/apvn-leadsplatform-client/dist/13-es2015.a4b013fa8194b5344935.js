(window.webpackJsonp=window.webpackJsonp||[]).push([[13],{"6dU7":function(a,c,e){"use strict";e.r(c),e.d(c,"DashboardModule",function(){return x});var i=e("3Pt+"),r=e("LPYB"),t=e("dZIy"),s=e("fXoL");const o={provide:i.k,useExisting:Object(s.gb)(()=>n),multi:!0};let n=(()=>{class a{constructor(){this.btnCheckboxTrue=!0,this.btnCheckboxFalse=!1,this.state=!1,this.onChange=Function.prototype,this.onTouched=Function.prototype}onClick(){this.isDisabled||(this.toggle(!this.state),this.onChange(this.value))}ngOnInit(){this.toggle(this.trueValue===this.value)}get trueValue(){return void 0===this.btnCheckboxTrue||this.btnCheckboxTrue}get falseValue(){return void 0!==this.btnCheckboxFalse&&this.btnCheckboxFalse}toggle(a){this.state=a,this.value=this.state?this.trueValue:this.falseValue}writeValue(a){this.state=this.trueValue===a,this.value=a?this.trueValue:this.falseValue}setDisabledState(a){this.isDisabled=a}registerOnChange(a){this.onChange=a}registerOnTouched(a){this.onTouched=a}}return a.\u0275fac=function(c){return new(c||a)},a.\u0275dir=s.Vb({type:a,selectors:[["","btnCheckbox",""]],hostVars:3,hostBindings:function(a,c){1&a&&s.nc("click",function(){return c.onClick()}),2&a&&(s.Pb("aria-pressed",c.state),s.Sb("active",c.state))},inputs:{btnCheckboxTrue:"btnCheckboxTrue",btnCheckboxFalse:"btnCheckboxFalse"},features:[s.Nb([o])]}),a})();const d={provide:i.k,useExisting:Object(s.gb)(()=>g),multi:!0};let g=(()=>{class a{constructor(a,c,e,i){this.el=a,this.cdr=c,this.group=e,this.renderer=i,this.onChange=Function.prototype,this.onTouched=Function.prototype}get value(){return this.group?this.group.value:this._value}set value(a){this.group?this.group.value=a:this._value=a}get disabled(){return this._disabled}set disabled(a){this._disabled=a,this.setDisabledState(a)}get isActive(){return this.btnRadio===this.value}onClick(){this.el.nativeElement.attributes.disabled||!this.uncheckable&&this.btnRadio===this.value||(this.value=this.uncheckable&&this.btnRadio===this.value?void 0:this.btnRadio,this._onChange(this.value))}ngOnInit(){this.uncheckable=void 0!==this.uncheckable}onBlur(){this.onTouched()}_onChange(a){if(this.group)return this.group.onTouched(),void this.group.onChange(a);this.onTouched(),this.onChange(a)}writeValue(a){this.value=a,this.cdr.markForCheck()}registerOnChange(a){this.onChange=a}registerOnTouched(a){this.onTouched=a}setDisabledState(a){a?this.renderer.setAttribute(this.el.nativeElement,"disabled","disabled"):this.renderer.removeAttribute(this.el.nativeElement,"disabled")}}return a.\u0275fac=function(c){return new(c||a)(s.ac(s.o),s.ac(s.i),s.ac(b,8),s.ac(s.P))},a.\u0275dir=s.Vb({type:a,selectors:[["","btnRadio",""]],hostVars:3,hostBindings:function(a,c){1&a&&s.nc("click",function(){return c.onClick()}),2&a&&(s.Pb("aria-pressed",c.isActive),s.Sb("active",c.isActive))},inputs:{value:"value",disabled:"disabled",uncheckable:"uncheckable",btnRadio:"btnRadio"},features:[s.Nb([d])]}),a})();const l={provide:i.k,useExisting:Object(s.gb)(()=>b),multi:!0};let b=(()=>{class a{constructor(a){this.cdr=a,this.onChange=Function.prototype,this.onTouched=Function.prototype}get value(){return this._value}set value(a){this._value=a}writeValue(a){this._value=a,this.cdr.markForCheck()}registerOnChange(a){this.onChange=a}registerOnTouched(a){this.onTouched=a}setDisabledState(a){this.radioButtons&&this.radioButtons.forEach(c=>{c.setDisabledState(a)})}}return a.\u0275fac=function(c){return new(c||a)(s.ac(s.i))},a.\u0275dir=s.Vb({type:a,selectors:[["","btnRadioGroup",""]],contentQueries:function(a,c,e){if(1&a&&s.Tb(e,g,0),2&a){let a;s.Hc(a=s.oc())&&(c.radioButtons=a)}},features:[s.Nb([l])]}),a})(),f=(()=>{class a{static forRoot(){return{ngModule:a,providers:[]}}}return a.\u0275fac=function(c){return new(c||a)},a.\u0275mod=s.Yb({type:a}),a.\u0275inj=s.Xb({}),a})();var v=e("tyNb"),h=e("NuRj"),u=e("H++W");function p(a,c){1&a&&(s.gc(0,"div",134),s.gc(1,"a",135),s.Uc(2,"Action"),s.fc(),s.gc(3,"a",135),s.Uc(4,"Another action"),s.fc(),s.gc(5,"a",135),s.Uc(6,"Something else here"),s.fc(),s.gc(7,"a",135),s.Uc(8,"Something else here"),s.fc(),s.fc())}function m(a,c){1&a&&(s.gc(0,"div",134),s.gc(1,"a",135),s.Uc(2,"Action"),s.fc(),s.gc(3,"a",135),s.Uc(4,"Another action"),s.fc(),s.gc(5,"a",135),s.Uc(6,"Something else here"),s.fc(),s.fc())}function C(a,c){1&a&&(s.gc(0,"div",134),s.gc(1,"a",135),s.Uc(2,"Action"),s.fc(),s.gc(3,"a",135),s.Uc(4,"Another action"),s.fc(),s.gc(5,"a",135),s.Uc(6,"Something else here"),s.fc(),s.fc())}const y=[{path:"",component:(()=>{class a{constructor(){this.radioModel="Month",this.lineChart1Data=[{data:[65,59,84,84,51,55,40],label:"Series A"}],this.lineChart1Labels=["January","February","March","April","May","June","July"],this.lineChart1Options={tooltips:{enabled:!1,custom:u.CustomTooltips},maintainAspectRatio:!1,scales:{xAxes:[{gridLines:{color:"transparent",zeroLineColor:"transparent"},ticks:{fontSize:2,fontColor:"transparent"}}],yAxes:[{display:!1,ticks:{display:!1,min:35,max:89}}]},elements:{line:{borderWidth:1},point:{radius:4,hitRadius:10,hoverRadius:4}},legend:{display:!1}},this.lineChart1Colours=[{backgroundColor:Object(h.getStyle)("--primary"),borderColor:"rgba(255,255,255,.55)"}],this.lineChart1Legend=!1,this.lineChart1Type="line",this.lineChart2Data=[{data:[1,18,9,17,34,22,11],label:"Series A"}],this.lineChart2Labels=["January","February","March","April","May","June","July"],this.lineChart2Options={tooltips:{enabled:!1,custom:u.CustomTooltips},maintainAspectRatio:!1,scales:{xAxes:[{gridLines:{color:"transparent",zeroLineColor:"transparent"},ticks:{fontSize:2,fontColor:"transparent"}}],yAxes:[{display:!1,ticks:{display:!1,min:-4,max:39}}]},elements:{line:{tension:1e-5,borderWidth:1},point:{radius:4,hitRadius:10,hoverRadius:4}},legend:{display:!1}},this.lineChart2Colours=[{backgroundColor:Object(h.getStyle)("--info"),borderColor:"rgba(255,255,255,.55)"}],this.lineChart2Legend=!1,this.lineChart2Type="line",this.lineChart3Data=[{data:[78,81,80,45,34,12,40],label:"Series A"}],this.lineChart3Labels=["January","February","March","April","May","June","July"],this.lineChart3Options={tooltips:{enabled:!1,custom:u.CustomTooltips},maintainAspectRatio:!1,scales:{xAxes:[{display:!1}],yAxes:[{display:!1}]},elements:{line:{borderWidth:2},point:{radius:0,hitRadius:10,hoverRadius:4}},legend:{display:!1}},this.lineChart3Colours=[{backgroundColor:"rgba(255,255,255,.2)",borderColor:"rgba(255,255,255,.55)"}],this.lineChart3Legend=!1,this.lineChart3Type="line",this.barChart1Data=[{data:[78,81,80,45,34,12,40,78,81,80,45,34,12,40,12,40],label:"Series A",barPercentage:.6}],this.barChart1Labels=["1","2","3","4","5","6","7","8","9","10","11","12","13","14","15","16"],this.barChart1Options={tooltips:{enabled:!1,custom:u.CustomTooltips},maintainAspectRatio:!1,scales:{xAxes:[{display:!1}],yAxes:[{display:!1}]},legend:{display:!1}},this.barChart1Colours=[{backgroundColor:"rgba(255,255,255,.3)",borderWidth:0}],this.barChart1Legend=!1,this.barChart1Type="bar",this.mainChartElements=27,this.mainChartData1=[],this.mainChartData2=[],this.mainChartData3=[],this.mainChartData=[{data:this.mainChartData1,label:"Current"},{data:this.mainChartData2,label:"Previous"},{data:this.mainChartData3,label:"BEP"}],this.mainChartLabels=["Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday","Monday","Thursday","Wednesday","Thursday","Friday","Saturday","Sunday"],this.mainChartOptions={tooltips:{enabled:!1,custom:u.CustomTooltips,intersect:!0,mode:"index",position:"nearest",callbacks:{labelColor:function(a,c){return{backgroundColor:c.data.datasets[a.datasetIndex].borderColor}}}},responsive:!0,maintainAspectRatio:!1,scales:{xAxes:[{gridLines:{drawOnChartArea:!1},ticks:{callback:function(a){return a.charAt(0)}}}],yAxes:[{ticks:{beginAtZero:!0,maxTicksLimit:5,stepSize:Math.ceil(50),max:250}}]},elements:{line:{borderWidth:2},point:{radius:0,hitRadius:10,hoverRadius:4,hoverBorderWidth:3}},legend:{display:!1}},this.mainChartColours=[{backgroundColor:Object(h.hexToRgba)(Object(h.getStyle)("--info"),10),borderColor:Object(h.getStyle)("--info"),pointHoverBackgroundColor:"#fff"},{backgroundColor:"transparent",borderColor:Object(h.getStyle)("--success"),pointHoverBackgroundColor:"#fff"},{backgroundColor:"transparent",borderColor:Object(h.getStyle)("--danger"),pointHoverBackgroundColor:"#fff",borderWidth:1,borderDash:[8,5]}],this.mainChartLegend=!1,this.mainChartType="line",this.brandBoxChartData1=[{data:[65,59,84,84,51,55,40],label:"Facebook"}],this.brandBoxChartData2=[{data:[1,13,9,17,34,41,38],label:"Twitter"}],this.brandBoxChartData3=[{data:[78,81,80,45,34,12,40],label:"LinkedIn"}],this.brandBoxChartData4=[{data:[35,23,56,22,97,23,64],label:"Google+"}],this.brandBoxChartLabels=["January","February","March","April","May","June","July"],this.brandBoxChartOptions={tooltips:{enabled:!1,custom:u.CustomTooltips},responsive:!0,maintainAspectRatio:!1,scales:{xAxes:[{display:!1}],yAxes:[{display:!1}]},elements:{line:{borderWidth:2},point:{radius:0,hitRadius:10,hoverRadius:4,hoverBorderWidth:3}},legend:{display:!1}},this.brandBoxChartColours=[{backgroundColor:"rgba(255,255,255,.1)",borderColor:"rgba(255,255,255,.55)",pointHoverBackgroundColor:"#fff"}],this.brandBoxChartLegend=!1,this.brandBoxChartType="line"}random(a,c){return Math.floor(Math.random()*(c-a+1)+a)}ngOnInit(){for(let a=0;a<=this.mainChartElements;a++)this.mainChartData1.push(this.random(50,200)),this.mainChartData2.push(this.random(80,100)),this.mainChartData3.push(65)}}return a.\u0275fac=function(c){return new(c||a)},a.\u0275cmp=s.Ub({type:a,selectors:[["ng-component"]],decls:553,vars:57,consts:[[1,"animated","fadeIn"],[1,"row"],[1,"col-sm-6","col-lg-3"],[1,"card","text-white","bg-info"],[1,"card-body","pb-0"],["type","button",1,"btn","btn-transparent","p-0","float-right"],[1,"icon-location-pin"],[1,"text-value"],[1,"chart-wrapper","mt-3","mx-3",2,"height","70px"],["baseChart","",1,"chart",3,"datasets","labels","options","colors","legend","chartType"],[1,"card","text-white","bg-primary"],["dropdown","",1,"btn-group","float-right"],["type","button","dropdownToggle","",1,"btn","btn-transparent","dropdown-toggle","p-0"],[1,"icon-settings"],["class","dropdown-menu dropdown-menu-right",4,"dropdownMenu"],[1,"card","text-white","bg-warning"],[1,"chart-wrapper","mt-3",2,"height","70px"],[1,"card","text-white","bg-danger"],[1,"card"],[1,"card-body"],[1,"col-sm-5"],[1,"card-title","mb-0"],[1,"small","text-muted"],[1,"col-sm-7","d-none","d-md-block"],["type","button",1,"btn","btn-primary","float-right"],[1,"icon-cloud-download"],["data-toggle","buttons",1,"btn-group","btn-group-toggle","float-right","mr-3"],["btnRadio","Day","id","option1",1,"btn","btn-outline-secondary",3,"ngModel","ngModelChange"],["btnRadio","Month","id","option2",1,"btn","btn-outline-secondary",3,"ngModel","ngModelChange"],["btnRadio","Year","id","option3",1,"btn","btn-outline-secondary",3,"ngModel","ngModelChange"],[1,"chart-wrapper",2,"height","300px","margin-top","40px"],[1,"card-footer"],[1,"row","text-center"],[1,"col-sm-12","col-md","mb-sm-2","mb-0"],[1,"text-muted"],[1,"progress","progress-xs","mt-2"],["role","progressbar","aria-valuenow","40","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-success",2,"width","40%"],["role","progressbar","aria-valuenow","20","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-info",2,"width","20%"],["role","progressbar","aria-valuenow","60","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-warning",2,"width","60%"],["role","progressbar","aria-valuenow","80","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-danger",2,"width","80%"],["role","progressbar","aria-valuenow","40","aria-valuemin","0","aria-valuemax","100",1,"progress-bar",2,"width","40%"],[1,"brand-card"],[1,"brand-card-header","bg-facebook"],[1,"fa","fa-facebook"],[1,"chart-wrapper"],[1,"brand-card-body"],[1,"text-uppercase","text-muted","small"],[1,"brand-card-header","bg-twitter"],[1,"fa","fa-twitter"],[1,"brand-card-header","bg-linkedin"],[1,"fa","fa-linkedin"],[1,"brand-card-header","bg-google-plus"],[1,"fa","fa-google-plus"],[1,"col-md-12"],[1,"card-header"],[1,"col-sm-6"],[1,"callout","callout-info"],[1,"h4"],[1,"callout","callout-danger"],[1,"mt-0"],[1,"progress-group","mb-4"],[1,"progress-group-prepend"],[1,"progress-group-text"],[1,"progress-group-bars"],[1,"progress","progress-xs"],["role","progressbar","aria-valuenow","34","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-info",2,"width","34%"],["role","progressbar","aria-valuenow","78","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-danger",2,"width","78%"],["role","progressbar","aria-valuenow","56","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-info",2,"width","56%"],["role","progressbar","aria-valuenow","94","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-danger",2,"width","94%"],["role","progressbar","aria-valuenow","12","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-info",2,"width","12%"],["role","progressbar","aria-valuenow","67","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-danger",2,"width","67%"],["role","progressbar","aria-valuenow","43","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-info",2,"width","43%"],["role","progressbar","aria-valuenow","91","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-danger",2,"width","91%"],["role","progressbar","aria-valuenow","22","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-info",2,"width","22%"],["role","progressbar","aria-valuenow","73","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-danger",2,"width","73%"],["role","progressbar","aria-valuenow","53","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-info",2,"width","53%"],["role","progressbar","aria-valuenow","82","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-danger",2,"width","82%"],["role","progressbar","aria-valuenow","9","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-info",2,"width","9%"],["role","progressbar","aria-valuenow","69","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-danger",2,"width","69%"],[1,"callout","callout-warning"],[1,"callout","callout-success"],[1,"progress-group"],[1,"progress-group-header"],[1,"icon-user","progress-group-icon"],[1,"ml-auto","font-weight-bold"],["role","progressbar","aria-valuenow","43","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-warning",2,"width","43%"],[1,"progress-group","mb-5"],[1,"icon-user-female","progress-group-icon"],[1,"progress-group-header","align-items-end"],[1,"icon-globe","progress-group-icon"],[1,"ml-auto","font-weight-bold","mr-2"],[1,"text-muted","small"],["role","progressbar","aria-valuenow","56","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-success",2,"width","56%"],[1,"icon-social-facebook","progress-group-icon"],["role","progressbar","aria-valuenow","15","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-success",2,"width","15%"],[1,"icon-social-twitter","progress-group-icon"],["role","progressbar","aria-valuenow","11","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-success",2,"width","11%"],[1,"icon-social-linkedin","progress-group-icon"],["role","progressbar","aria-valuenow","8","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-success",2,"width","8%"],[1,"table","table-responsive-sm","table-hover","table-outline","mb-0"],[1,"thead-light"],[1,"text-center"],[1,"icon-people"],[1,"avatar"],["src","assets/img/avatars/1.jpg","alt","admin@bootstrapmaster.com",1,"img-avatar"],[1,"avatar-status","badge-success"],["title","us","id","us",1,"flag-icon","flag-icon-us","h4","mb-0"],[1,"clearfix"],[1,"float-left"],[1,"float-right"],["role","progressbar","aria-valuenow","50","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-success",2,"width","50%"],[1,"fa","fa-cc-mastercard",2,"font-size","24px"],["src","assets/img/avatars/2.jpg","alt","admin@bootstrapmaster.com",1,"img-avatar"],[1,"avatar-status","badge-danger"],["title","br","id","br",1,"flag-icon","flag-icon-br","h4","mb-0"],["role","progressbar","aria-valuenow","10","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-info",2,"width","10%"],[1,"fa","fa-cc-visa",2,"font-size","24px"],["src","assets/img/avatars/3.jpg","alt","admin@bootstrapmaster.com",1,"img-avatar"],[1,"avatar-status","badge-warning"],["title","in","id","in",1,"flag-icon","flag-icon-in","h4","mb-0"],["role","progressbar","aria-valuenow","74","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-warning",2,"width","74%"],[1,"fa","fa-cc-stripe",2,"font-size","24px"],["src","assets/img/avatars/4.jpg","alt","admin@bootstrapmaster.com",1,"img-avatar"],[1,"avatar-status","badge-secondary"],["title","fr","id","fr",1,"flag-icon","flag-icon-fr","h4","mb-0"],["role","progressbar","aria-valuenow","98","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-danger",2,"width","98%"],[1,"fa","fa-paypal",2,"font-size","24px"],["src","assets/img/avatars/5.jpg","alt","admin@bootstrapmaster.com",1,"img-avatar"],["title","es","id","es",1,"flag-icon","flag-icon-es","h4","mb-0"],[1,"fa","fa-google-wallet",2,"font-size","24px"],["src","assets/img/avatars/6.jpg","alt","admin@bootstrapmaster.com",1,"img-avatar"],["title","pl","id","pl",1,"flag-icon","flag-icon-pl","h4","mb-0"],["role","progressbar","aria-valuenow","43","aria-valuemin","0","aria-valuemax","100",1,"progress-bar","bg-success",2,"width","43%"],[1,"fa","fa-cc-amex",2,"font-size","24px"],[1,"dropdown-menu","dropdown-menu-right"],["href","#",1,"dropdown-item"]],template:function(a,c){if(1&a){s.gc(0,"div",0),s.gc(1,"div",1),s.gc(2,"div",2),s.gc(3,"div",3),s.gc(4,"div",4),s.gc(5,"button",5),s.bc(6,"i",6),s.fc(),s.gc(7,"div",7),s.Uc(8,"9.823"),s.fc(),s.gc(9,"div"),s.Uc(10,"Members online"),s.fc(),s.fc(),s.gc(11,"div",8),s.bc(12,"canvas",9),s.fc(),s.fc(),s.fc(),s.gc(13,"div",2),s.gc(14,"div",10),s.gc(15,"div",4),s.gc(16,"div",11),s.gc(17,"button",12),s.bc(18,"i",13),s.fc(),s.Sc(19,p,9,0,"div",14),s.fc(),s.gc(20,"div",7),s.Uc(21,"9.823"),s.fc(),s.gc(22,"div"),s.Uc(23,"Members online"),s.fc(),s.fc(),s.gc(24,"div",8),s.bc(25,"canvas",9),s.fc(),s.fc(),s.fc(),s.gc(26,"div",2),s.gc(27,"div",15),s.gc(28,"div",4),s.gc(29,"div",11),s.gc(30,"button",12),s.bc(31,"i",13),s.fc(),s.Sc(32,m,7,0,"div",14),s.fc(),s.gc(33,"div",7),s.Uc(34,"9.823"),s.fc(),s.gc(35,"div"),s.Uc(36,"Members online"),s.fc(),s.fc(),s.gc(37,"div",16),s.bc(38,"canvas",9),s.fc(),s.fc(),s.fc(),s.gc(39,"div",2),s.gc(40,"div",17),s.gc(41,"div",4),s.gc(42,"div",11),s.gc(43,"button",12),s.bc(44,"i",13),s.fc(),s.Sc(45,C,7,0,"div",14),s.fc(),s.gc(46,"div",7),s.Uc(47,"9.823"),s.fc(),s.gc(48,"div"),s.Uc(49,"Members online"),s.fc(),s.fc(),s.gc(50,"div",8),s.bc(51,"canvas",9),s.fc(),s.fc(),s.fc(),s.fc(),s.gc(52,"div",18),s.gc(53,"div",19),s.gc(54,"div",1),s.gc(55,"div",20),s.gc(56,"h4",21),s.Uc(57,"Traffic"),s.fc(),s.gc(58,"div",22),s.Uc(59,"November 2017"),s.fc(),s.fc(),s.gc(60,"div",23),s.gc(61,"button",24),s.bc(62,"i",25),s.fc(),s.gc(63,"div",26),s.gc(64,"label",27),s.nc("ngModelChange",function(a){return c.radioModel=a}),s.Uc(65,"Day"),s.fc(),s.gc(66,"label",28),s.nc("ngModelChange",function(a){return c.radioModel=a}),s.Uc(67,"Month"),s.fc(),s.gc(68,"label",29),s.nc("ngModelChange",function(a){return c.radioModel=a}),s.Uc(69,"Year"),s.fc(),s.fc(),s.fc(),s.fc(),s.gc(70,"div",30),s.bc(71,"canvas",9),s.fc(),s.fc(),s.gc(72,"div",31),s.gc(73,"div",32),s.gc(74,"div",33),s.gc(75,"div",34),s.Uc(76,"Visits"),s.fc(),s.gc(77,"strong"),s.Uc(78,"29.703 Users (40%)"),s.fc(),s.gc(79,"div",35),s.bc(80,"div",36),s.fc(),s.fc(),s.gc(81,"div",33),s.gc(82,"div",34),s.Uc(83,"Unique"),s.fc(),s.gc(84,"strong"),s.Uc(85,"24.093 Users (20%)"),s.fc(),s.gc(86,"div",35),s.bc(87,"div",37),s.fc(),s.fc(),s.gc(88,"div",33),s.gc(89,"div",34),s.Uc(90,"Pageviews"),s.fc(),s.gc(91,"strong"),s.Uc(92,"78.706 Views (60%)"),s.fc(),s.gc(93,"div",35),s.bc(94,"div",38),s.fc(),s.fc(),s.gc(95,"div",33),s.gc(96,"div",34),s.Uc(97,"New Users"),s.fc(),s.gc(98,"strong"),s.Uc(99,"22.123 Users (80%)"),s.fc(),s.gc(100,"div",35),s.bc(101,"div",39),s.fc(),s.fc(),s.gc(102,"div",33),s.gc(103,"div",34),s.Uc(104,"Bounce Rate"),s.fc(),s.gc(105,"strong"),s.Uc(106,"40.15%"),s.fc(),s.gc(107,"div",35),s.bc(108,"div",40),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.gc(109,"div",1),s.gc(110,"div",2),s.gc(111,"div",41),s.gc(112,"div",42),s.bc(113,"i",43),s.gc(114,"div",44),s.bc(115,"canvas",9),s.fc(),s.fc(),s.gc(116,"div",45),s.gc(117,"div"),s.gc(118,"div",7),s.Uc(119,"89k"),s.fc(),s.gc(120,"div",46),s.Uc(121,"friends"),s.fc(),s.fc(),s.gc(122,"div"),s.gc(123,"div",7),s.Uc(124,"459"),s.fc(),s.gc(125,"div",46),s.Uc(126,"feeds"),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.gc(127,"div",2),s.gc(128,"div",41),s.gc(129,"div",47),s.bc(130,"i",48),s.gc(131,"div",44),s.bc(132,"canvas",9),s.fc(),s.fc(),s.gc(133,"div",45),s.gc(134,"div"),s.gc(135,"div",7),s.Uc(136,"973k"),s.fc(),s.gc(137,"div",46),s.Uc(138,"followers"),s.fc(),s.fc(),s.gc(139,"div"),s.gc(140,"div",7),s.Uc(141,"1.792"),s.fc(),s.gc(142,"div",46),s.Uc(143,"tweets"),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.gc(144,"div",2),s.gc(145,"div",41),s.gc(146,"div",49),s.bc(147,"i",50),s.gc(148,"div",44),s.bc(149,"canvas",9),s.fc(),s.fc(),s.gc(150,"div",45),s.gc(151,"div"),s.gc(152,"div",7),s.Uc(153,"500+"),s.fc(),s.gc(154,"div",46),s.Uc(155,"contacts"),s.fc(),s.fc(),s.gc(156,"div"),s.gc(157,"div",7),s.Uc(158,"292"),s.fc(),s.gc(159,"div",46),s.Uc(160,"feeds"),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.gc(161,"div",2),s.gc(162,"div",41),s.gc(163,"div",51),s.bc(164,"i",52),s.gc(165,"div",44),s.bc(166,"canvas",9),s.fc(),s.fc(),s.gc(167,"div",45),s.gc(168,"div"),s.gc(169,"div",7),s.Uc(170,"894"),s.fc(),s.gc(171,"div",46),s.Uc(172,"followers"),s.fc(),s.fc(),s.gc(173,"div"),s.gc(174,"div",7),s.Uc(175,"92"),s.fc(),s.gc(176,"div",46),s.Uc(177,"circles"),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.gc(178,"div",1),s.gc(179,"div",53),s.gc(180,"div",18),s.gc(181,"div",54),s.Uc(182," Traffic & Sales "),s.fc(),s.gc(183,"div",19),s.gc(184,"div",1),s.gc(185,"div",55),s.gc(186,"div",1),s.gc(187,"div",55),s.gc(188,"div",56),s.gc(189,"small",34),s.Uc(190,"New Clients"),s.fc(),s.bc(191,"br"),s.gc(192,"strong",57),s.Uc(193,"9,123"),s.fc(),s.fc(),s.fc(),s.gc(194,"div",55),s.gc(195,"div",58),s.gc(196,"small",34),s.Uc(197,"Recuring Clients"),s.fc(),s.bc(198,"br"),s.gc(199,"strong",57),s.Uc(200,"22,643"),s.fc(),s.fc(),s.fc(),s.fc(),s.bc(201,"hr",59),s.gc(202,"div",60),s.gc(203,"div",61),s.gc(204,"span",62),s.Uc(205," Monday "),s.fc(),s.fc(),s.gc(206,"div",63),s.gc(207,"div",64),s.bc(208,"div",65),s.fc(),s.gc(209,"div",64),s.bc(210,"div",66),s.fc(),s.fc(),s.fc(),s.gc(211,"div",60),s.gc(212,"div",61),s.gc(213,"span",62),s.Uc(214," Tuesday "),s.fc(),s.fc(),s.gc(215,"div",63),s.gc(216,"div",64),s.bc(217,"div",67),s.fc(),s.gc(218,"div",64),s.bc(219,"div",68),s.fc(),s.fc(),s.fc(),s.gc(220,"div",60),s.gc(221,"div",61),s.gc(222,"span",62),s.Uc(223," Wednesday "),s.fc(),s.fc(),s.gc(224,"div",63),s.gc(225,"div",64),s.bc(226,"div",69),s.fc(),s.gc(227,"div",64),s.bc(228,"div",70),s.fc(),s.fc(),s.fc(),s.gc(229,"div",60),s.gc(230,"div",61),s.gc(231,"span",62),s.Uc(232," Thursday "),s.fc(),s.fc(),s.gc(233,"div",63),s.gc(234,"div",64),s.bc(235,"div",71),s.fc(),s.gc(236,"div",64),s.bc(237,"div",72),s.fc(),s.fc(),s.fc(),s.gc(238,"div",60),s.gc(239,"div",61),s.gc(240,"span",62),s.Uc(241," Friday "),s.fc(),s.fc(),s.gc(242,"div",63),s.gc(243,"div",64),s.bc(244,"div",73),s.fc(),s.gc(245,"div",64),s.bc(246,"div",74),s.fc(),s.fc(),s.fc(),s.gc(247,"div",60),s.gc(248,"div",61),s.gc(249,"span",62),s.Uc(250," Saturday "),s.fc(),s.fc(),s.gc(251,"div",63),s.gc(252,"div",64),s.bc(253,"div",75),s.fc(),s.gc(254,"div",64),s.bc(255,"div",76),s.fc(),s.fc(),s.fc(),s.gc(256,"div",60),s.gc(257,"div",61),s.gc(258,"span",62),s.Uc(259," Sunday "),s.fc(),s.fc(),s.gc(260,"div",63),s.gc(261,"div",64),s.bc(262,"div",77),s.fc(),s.gc(263,"div",64),s.bc(264,"div",78),s.fc(),s.fc(),s.fc(),s.fc(),s.gc(265,"div",55),s.gc(266,"div",1),s.gc(267,"div",55),s.gc(268,"div",79),s.gc(269,"small",34),s.Uc(270,"Pageviews"),s.fc(),s.bc(271,"br"),s.gc(272,"strong",57),s.Uc(273,"78,623"),s.fc(),s.fc(),s.fc(),s.gc(274,"div",55),s.gc(275,"div",80),s.gc(276,"small",34),s.Uc(277,"Organic"),s.fc(),s.bc(278,"br"),s.gc(279,"strong",57),s.Uc(280,"49,123"),s.fc(),s.fc(),s.fc(),s.fc(),s.bc(281,"hr",59),s.gc(282,"div",81),s.gc(283,"div",82),s.bc(284,"i",83),s.gc(285,"div"),s.Uc(286,"Male"),s.fc(),s.gc(287,"div",84),s.Uc(288,"43%"),s.fc(),s.fc(),s.gc(289,"div",63),s.gc(290,"div",64),s.bc(291,"div",85),s.fc(),s.fc(),s.fc(),s.gc(292,"div",86),s.gc(293,"div",82),s.bc(294,"i",87),s.gc(295,"div"),s.Uc(296,"Female"),s.fc(),s.gc(297,"div",84),s.Uc(298,"37%"),s.fc(),s.fc(),s.gc(299,"div",63),s.gc(300,"div",64),s.bc(301,"div",85),s.fc(),s.fc(),s.fc(),s.gc(302,"div",81),s.gc(303,"div",88),s.bc(304,"i",89),s.gc(305,"div"),s.Uc(306,"Organic Search"),s.fc(),s.gc(307,"div",90),s.Uc(308,"191.235"),s.fc(),s.gc(309,"div",91),s.Uc(310,"(56%)"),s.fc(),s.fc(),s.gc(311,"div",63),s.gc(312,"div",64),s.bc(313,"div",92),s.fc(),s.fc(),s.fc(),s.gc(314,"div",81),s.gc(315,"div",88),s.bc(316,"i",93),s.gc(317,"div"),s.Uc(318,"Facebook"),s.fc(),s.gc(319,"div",90),s.Uc(320,"51.223"),s.fc(),s.gc(321,"div",91),s.Uc(322,"(15%)"),s.fc(),s.fc(),s.gc(323,"div",63),s.gc(324,"div",64),s.bc(325,"div",94),s.fc(),s.fc(),s.fc(),s.gc(326,"div",81),s.gc(327,"div",88),s.bc(328,"i",95),s.gc(329,"div"),s.Uc(330,"Twitter"),s.fc(),s.gc(331,"div",90),s.Uc(332,"37.564"),s.fc(),s.gc(333,"div",91),s.Uc(334,"(11%)"),s.fc(),s.fc(),s.gc(335,"div",63),s.gc(336,"div",64),s.bc(337,"div",96),s.fc(),s.fc(),s.fc(),s.gc(338,"div",81),s.gc(339,"div",88),s.bc(340,"i",97),s.gc(341,"div"),s.Uc(342,"LinkedIn"),s.fc(),s.gc(343,"div",90),s.Uc(344,"27.319"),s.fc(),s.gc(345,"div",91),s.Uc(346,"(8%)"),s.fc(),s.fc(),s.gc(347,"div",63),s.gc(348,"div",64),s.bc(349,"div",98),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.bc(350,"br"),s.gc(351,"table",99),s.gc(352,"thead",100),s.gc(353,"tr"),s.gc(354,"th",101),s.bc(355,"i",102),s.fc(),s.gc(356,"th"),s.Uc(357,"User"),s.fc(),s.gc(358,"th",101),s.Uc(359,"Country"),s.fc(),s.gc(360,"th"),s.Uc(361,"Usage"),s.fc(),s.gc(362,"th",101),s.Uc(363,"Payment Method"),s.fc(),s.gc(364,"th"),s.Uc(365,"Activity"),s.fc(),s.fc(),s.fc(),s.gc(366,"tbody"),s.gc(367,"tr"),s.gc(368,"td",101),s.gc(369,"div",103),s.bc(370,"img",104),s.bc(371,"span",105),s.fc(),s.fc(),s.gc(372,"td"),s.gc(373,"div"),s.Uc(374,"Yiorgos Avraamu"),s.fc(),s.gc(375,"div",22),s.gc(376,"span"),s.Uc(377,"New"),s.fc(),s.Uc(378," | Registered: Jan 1, 2015 "),s.fc(),s.fc(),s.gc(379,"td",101),s.bc(380,"i",106),s.fc(),s.gc(381,"td"),s.gc(382,"div",107),s.gc(383,"div",108),s.gc(384,"strong"),s.Uc(385,"50%"),s.fc(),s.fc(),s.gc(386,"div",109),s.gc(387,"small",34),s.Uc(388,"Jun 11, 2015 - Jul 10, 2015"),s.fc(),s.fc(),s.fc(),s.gc(389,"div",64),s.bc(390,"div",110),s.fc(),s.fc(),s.gc(391,"td",101),s.bc(392,"i",111),s.fc(),s.gc(393,"td"),s.gc(394,"div",22),s.Uc(395,"Last login"),s.fc(),s.gc(396,"strong"),s.Uc(397,"10 sec ago"),s.fc(),s.fc(),s.fc(),s.gc(398,"tr"),s.gc(399,"td",101),s.gc(400,"div",103),s.bc(401,"img",112),s.bc(402,"span",113),s.fc(),s.fc(),s.gc(403,"td"),s.gc(404,"div"),s.Uc(405,"Avram Tarasios"),s.fc(),s.gc(406,"div",22),s.gc(407,"span"),s.Uc(408,"Recurring"),s.fc(),s.Uc(409," | Registered: Jan 1, 2015 "),s.fc(),s.fc(),s.gc(410,"td",101),s.bc(411,"i",114),s.fc(),s.gc(412,"td"),s.gc(413,"div",107),s.gc(414,"div",108),s.gc(415,"strong"),s.Uc(416,"10%"),s.fc(),s.fc(),s.gc(417,"div",109),s.gc(418,"small",34),s.Uc(419,"Jun 11, 2015 - Jul 10, 2015"),s.fc(),s.fc(),s.fc(),s.gc(420,"div",64),s.bc(421,"div",115),s.fc(),s.fc(),s.gc(422,"td",101),s.bc(423,"i",116),s.fc(),s.gc(424,"td"),s.gc(425,"div",22),s.Uc(426,"Last login"),s.fc(),s.gc(427,"strong"),s.Uc(428,"5 minutes ago"),s.fc(),s.fc(),s.fc(),s.gc(429,"tr"),s.gc(430,"td",101),s.gc(431,"div",103),s.bc(432,"img",117),s.bc(433,"span",118),s.fc(),s.fc(),s.gc(434,"td"),s.gc(435,"div"),s.Uc(436,"Quintin Ed"),s.fc(),s.gc(437,"div",22),s.gc(438,"span"),s.Uc(439,"New"),s.fc(),s.Uc(440," | Registered: Jan 1, 2015 "),s.fc(),s.fc(),s.gc(441,"td",101),s.bc(442,"i",119),s.fc(),s.gc(443,"td"),s.gc(444,"div",107),s.gc(445,"div",108),s.gc(446,"strong"),s.Uc(447,"74%"),s.fc(),s.fc(),s.gc(448,"div",109),s.gc(449,"small",34),s.Uc(450,"Jun 11, 2015 - Jul 10, 2015"),s.fc(),s.fc(),s.fc(),s.gc(451,"div",64),s.bc(452,"div",120),s.fc(),s.fc(),s.gc(453,"td",101),s.bc(454,"i",121),s.fc(),s.gc(455,"td"),s.gc(456,"div",22),s.Uc(457,"Last login"),s.fc(),s.gc(458,"strong"),s.Uc(459,"1 hour ago"),s.fc(),s.fc(),s.fc(),s.gc(460,"tr"),s.gc(461,"td",101),s.gc(462,"div",103),s.bc(463,"img",122),s.bc(464,"span",123),s.fc(),s.fc(),s.gc(465,"td"),s.gc(466,"div"),s.Uc(467,"En\xe9as Kwadwo"),s.fc(),s.gc(468,"div",22),s.gc(469,"span"),s.Uc(470,"New"),s.fc(),s.Uc(471," | Registered: Jan 1, 2015 "),s.fc(),s.fc(),s.gc(472,"td",101),s.bc(473,"i",124),s.fc(),s.gc(474,"td"),s.gc(475,"div",107),s.gc(476,"div",108),s.gc(477,"strong"),s.Uc(478,"98%"),s.fc(),s.fc(),s.gc(479,"div",109),s.gc(480,"small",34),s.Uc(481,"Jun 11, 2015 - Jul 10, 2015"),s.fc(),s.fc(),s.fc(),s.gc(482,"div",64),s.bc(483,"div",125),s.fc(),s.fc(),s.gc(484,"td",101),s.bc(485,"i",126),s.fc(),s.gc(486,"td"),s.gc(487,"div",22),s.Uc(488,"Last login"),s.fc(),s.gc(489,"strong"),s.Uc(490,"Last month"),s.fc(),s.fc(),s.fc(),s.gc(491,"tr"),s.gc(492,"td",101),s.gc(493,"div",103),s.bc(494,"img",127),s.bc(495,"span",105),s.fc();s.fc(),s.gc(496,"td"),s.gc(497,"div"),s.Uc(498,"Agapetus Tade\xe1\u0161"),s.fc(),s.gc(499,"div",22),s.gc(500,"span"),s.Uc(501,"New"),s.fc(),s.Uc(502," | Registered: Jan 1, 2015 "),s.fc(),s.fc(),s.gc(503,"td",101),s.bc(504,"i",128),s.fc(),s.gc(505,"td"),s.gc(506,"div",107),s.gc(507,"div",108),s.gc(508,"strong"),s.Uc(509,"22%"),s.fc(),s.fc(),s.gc(510,"div",109),s.gc(511,"small",34),s.Uc(512,"Jun 11, 2015 - Jul 10, 2015"),s.fc(),s.fc(),s.fc(),s.gc(513,"div",64),s.bc(514,"div",73),s.fc(),s.fc(),s.gc(515,"td",101),s.bc(516,"i",129),s.fc(),s.gc(517,"td"),s.gc(518,"div",22),s.Uc(519,"Last login"),s.fc(),s.gc(520,"strong"),s.Uc(521,"Last week"),s.fc(),s.fc(),s.fc(),s.gc(522,"tr"),s.gc(523,"td",101),s.gc(524,"div",103),s.bc(525,"img",130),s.bc(526,"span",113),s.fc(),s.fc(),s.gc(527,"td"),s.gc(528,"div"),s.Uc(529,"Friderik D\xe1vid"),s.fc(),s.gc(530,"div",22),s.gc(531,"span"),s.Uc(532,"New"),s.fc(),s.Uc(533," | Registered: Jan 1, 2015 "),s.fc(),s.fc(),s.gc(534,"td",101),s.bc(535,"i",131),s.fc(),s.gc(536,"td"),s.gc(537,"div",107),s.gc(538,"div",108),s.gc(539,"strong"),s.Uc(540,"43%"),s.fc(),s.fc(),s.gc(541,"div",109),s.gc(542,"small",34),s.Uc(543,"Jun 11, 2015 - Jul 10, 2015"),s.fc(),s.fc(),s.fc(),s.gc(544,"div",64),s.bc(545,"div",132),s.fc(),s.fc(),s.gc(546,"td",101),s.bc(547,"i",133),s.fc(),s.gc(548,"td"),s.gc(549,"div",22),s.Uc(550,"Last login"),s.fc(),s.gc(551,"strong"),s.Uc(552,"Yesterday"),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.fc(),s.fc()}2&a&&(s.Ob(12),s.yc("datasets",c.lineChart2Data)("labels",c.lineChart2Labels)("options",c.lineChart2Options)("colors",c.lineChart2Colours)("legend",c.lineChart2Legend)("chartType",c.lineChart2Type),s.Ob(13),s.yc("datasets",c.lineChart1Data)("labels",c.lineChart1Labels)("options",c.lineChart1Options)("colors",c.lineChart1Colours)("legend",c.lineChart1Legend)("chartType",c.lineChart1Type),s.Ob(13),s.yc("datasets",c.lineChart3Data)("labels",c.lineChart3Labels)("options",c.lineChart3Options)("colors",c.lineChart3Colours)("legend",c.lineChart3Legend)("chartType",c.lineChart3Type),s.Ob(13),s.yc("datasets",c.barChart1Data)("labels",c.barChart1Labels)("options",c.barChart1Options)("colors",c.barChart1Colours)("legend",c.barChart1Legend)("chartType",c.barChart1Type),s.Ob(13),s.yc("ngModel",c.radioModel),s.Ob(2),s.yc("ngModel",c.radioModel),s.Ob(2),s.yc("ngModel",c.radioModel),s.Ob(3),s.yc("datasets",c.mainChartData)("labels",c.mainChartLabels)("options",c.mainChartOptions)("colors",c.mainChartColours)("legend",c.mainChartLegend)("chartType",c.mainChartType),s.Ob(44),s.yc("datasets",c.brandBoxChartData1)("labels",c.brandBoxChartLabels)("options",c.brandBoxChartOptions)("colors",c.brandBoxChartColours)("legend",c.brandBoxChartLegend)("chartType",c.brandBoxChartType),s.Ob(17),s.yc("datasets",c.brandBoxChartData2)("labels",c.brandBoxChartLabels)("options",c.brandBoxChartOptions)("colors",c.brandBoxChartColours)("legend",c.brandBoxChartLegend)("chartType",c.brandBoxChartType),s.Ob(17),s.yc("datasets",c.brandBoxChartData3)("labels",c.brandBoxChartLabels)("options",c.brandBoxChartOptions)("colors",c.brandBoxChartColours)("legend",c.brandBoxChartLegend)("chartType",c.brandBoxChartType),s.Ob(17),s.yc("datasets",c.brandBoxChartData4)("labels",c.brandBoxChartLabels)("options",c.brandBoxChartOptions)("colors",c.brandBoxChartColours)("legend",c.brandBoxChartLegend)("chartType",c.brandBoxChartType))},directives:[r.a,t.a,t.d,t.b,g,i.m,i.p],encapsulation:2}),a})(),data:{title:"Dashboard"}}];let U=(()=>{class a{}return a.\u0275fac=function(c){return new(c||a)},a.\u0275mod=s.Yb({type:a}),a.\u0275inj=s.Xb({imports:[[v.g.forChild(y)],v.g]}),a})(),x=(()=>{class a{}return a.\u0275fac=function(c){return new(c||a)},a.\u0275mod=s.Yb({type:a}),a.\u0275inj=s.Xb({imports:[[i.h,U,r.b,t.c,f.forRoot()]]}),a})()}}]);