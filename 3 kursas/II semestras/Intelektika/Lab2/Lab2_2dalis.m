clear;
clear all;
close all;
load sunspot.txt;
%Nubraizome pirmaji grafika
figure(1);
plot(sunspot(:,1),sunspot(:,2),'r-*');
xlabel('Metai');
ylabel('Demiu skaicius');
title('Saules demiu aktyvumas');
 
%Braizome antraji grafika
L = length(sunspot);
P = [
    sunspot(1:L-2,2)'
    sunspot(2:L-1,2)'
];
T = sunspot(3:L,2)';

%Apmokymo duomenys
Pu = P(:,1:200);
%Apmokymo rezultatai
Tu = T(:,1:200);

lr = 0.000001;
S = Tu;
net = newlin(Pu, S, 0, lr);
net.trainParam.goal = 100;
net.trainParam.epochs = 1000;
net = train(net, Pu, S);
%Isspausdinti reiksmes
disp('neurono svorio koeficientai:' );
disp( net.IW{1} );
disp( net.b{1} );
%Priskirti reiksmes
w1 = net.IW{1}(1);
w2 = net.IW{1}(2);
b = net.b{1};

%Prognozuojame aktyvuma
Tsu = net(Pu);
%Verifikuojame 1702-1901
figure(3);
hold on;
plot(sunspot(3:202, 1), Tsu, 'b-o');
plot(sunspot(3:202, 1), Tu, 'g-o');
xlabel('Metai');
ylabel('Demiu skaicius');
legend('Prognozuojamos demiu reiksmes', 'Tikrosios demiu reiksmes');
title('Saules demiu prognozes verifikavimas, 1710-1909');

%Prognozuojame aktyvuma
Ts = net(P);
%Verifikuojame 1702-2014
figure(4);
hold on;
plot(sunspot(3:315, 1), Ts, 'b-o');
plot(sunspot(3:315, 1), T, 'g-o');
xlabel('Metai');
ylabel('Demiu skaicius');
legend('Prognozuojamos demiu reiksmes', 'Tikrosios demiu reiksmes');
title('Saules demiu prognozes verifikavimas, 1710-2014');

%Prognoziu klaidingumo grafikas
E = T - Ts;
figure(5);
plot(sunspot(3:315), E, 'r-o');
title('Prognozes klaidos grafikas 1710-2014 metams');
xlabel('Metai');
ylabel('Klaidos rodiklis');

%Prognoziu klaidingumo histograma
figure(6);
hist(E);
title('Prognozes klaidingumo histograma');
xlabel('Klaidos rodiklis');
ylabel('Daznis');

%Apskaiciuojamas mse
MSE = mse(E);
disp('MSE:' );
disp(MSE);
