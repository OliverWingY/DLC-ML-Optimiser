function [successfull] = BuildSvmModel()
successfull = false;

DepositionTime = [45.12278754;	36.0433069;	45.14334973;	47.50963872;	40.07774017;	37.10612236;	46.11791178;	42.2766028;	50.09629298;	41.1563868];
MicrowavePower = [1050;	1100;	1050;	1100;	1200;	1200;	1200;	1050;	1050;	1100];
WorkingPressure = [0.012;	0.011;	0.011;	0.009;	0.009;	0.011;	0.011;	0.013;	0.011;	0.013];
GasFlowRateRatio = [94;	92;	85;	40;	74;	42;	91;	43;	66;	67];
Hardness = [4.9;	4.1;	3.4;	2.1;	3.9;	2.5;	4.2;	2.2;	3.3; 4.7];

CoatingData = table(DepositionTime,MicrowavePower,WorkingPressure, GasFlowRateRatio, Hardness);
%CoatingData = table(MicrowavePower,WorkingPressure, GasFlowRateRatio, Hardness);
SvmModel = fitrsvm(CoatingData, 'Hardness', 'KernelFunction','gaussian', 'Standardize',true);

assignin('base', 'SvmModel', SvmModel)
successfull = true;