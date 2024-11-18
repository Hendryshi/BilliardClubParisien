import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';

interface ApplicationDetail {
  id: number;
  nom: string;
  prenom: string;
  email: string;
  telephone: string;
  sexe: string;
  formule: string;
  previousMember: string;
  competition: string;
  categories?: {
    junior: boolean;
    mixte: boolean;
    feminine: boolean;
    regional: boolean;
    national: boolean;
  };
  motivation?: string;
  status: string;
}

@Component({
  selector: 'app-application-detail',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatCardModule,
    MatDividerModule,
    MatIconModule,
    MatButtonModule,
    MatChipsModule
  ],
  templateUrl: './application-detail.component.html',
  styleUrls: ['./application-detail.component.css']
})
export class ApplicationDetailComponent implements OnInit {
  applicationId: number = 0;
  application: ApplicationDetail = {
    id: 1,
    nom: 'Dupont',
    prenom: 'Jean',
    email: 'jean.dupont@email.com',
    telephone: '0612345678',
    sexe: 'M',
    formule: 'Gold',
    previousMember: 'Non',
    competition: 'Oui',
    categories: {
      junior: true,
      mixte: true,
      feminine: false,
      regional: true,
      national: true
    },
    motivation: `En tant que passionné de billard depuis mon plus jeune âge, je souhaite rejoindre votre club prestigieux pour plusieurs raisons. Tout d'abord, ma motivation principale est de progresser techniquement et tactiquement dans ce sport que j'affectionne particulièrement. J'ai commencé à jouer il y a environ cinq ans et j'ai rapidement développé une véritable passion pour cette discipline qui allie précision, stratégie et concentration.

À court terme, mon objectif est de perfectionner ma technique de base, notamment en travaillant sur la précision de mes coups et la maîtrise de l'effet. Je souhaite également améliorer ma lecture du jeu et ma capacité à construire des séries plus longues. Pour cela, je suis prêt à m'investir pleinement dans les entraînements et à suivre les conseils des joueurs plus expérimentés du club.

À moyen terme, je vise une progression significative de mon niveau de jeu pour pouvoir participer activement aux compétitions régionales. J'aimerais représenter le club dans différents tournois et contribuer à ses succès sportifs. Je suis particulièrement intéressé par les compétitions par équipes, car j'apprécie l'aspect collectif et l'esprit d'équipe qu'elles développent.

Sur le long terme, mon ambition est d'atteindre un niveau national et de participer aux championnats de France. Je sais que cela demande beaucoup de travail et de persévérance, mais je suis déterminé à mettre en œuvre tous les moyens nécessaires pour y parvenir. Je souhaite également partager ma passion avec les autres membres du club et, pourquoi pas, m'impliquer dans la formation des jeunes joueurs une fois que j'aurai acquis suffisamment d'expérience.

Cette saison, je souhaite participer à au moins six tournois pour acquérir de l'expérience en compétition. Je suis conscient que cela représente un engagement important, mais je suis prêt à organiser mon temps en conséquence. Mon objectif est d'obtenir des résultats significatifs dans ma catégorie et de progresser dans le classement national.

Je suis particulièrement attiré par votre club pour sa réputation d'excellence, la qualité de ses installations et l'encadrement professionnel qu'il propose. Je suis convaincu que c'est l'environnement idéal pour progresser et atteindre mes objectifs sportifs.`,
    status: 'En attente'
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.applicationId = Number(this.route.snapshot.paramMap.get('id'));
    // TODO: 使用 ID 从服务获取申请详情
  }

  getStatusClass(status: string): string {
    switch (status) {
      case 'En attente':
        return 'status-pending';
      case 'Approuvé':
        return 'status-approved';
      case 'Refusé':
        return 'status-rejected';
      default:
        return '';
    }
  }

  goBack(): void {
    this.router.navigate(['/admin/applications']);
  }

  approveApplication() {
    // TODO: 实现批准逻辑
    console.log('Approving application:', this.applicationId);
  }

  rejectApplication() {
    // TODO: 实现拒绝逻辑
    console.log('Rejecting application:', this.applicationId);
  }
} 