import { Component, OnInit } from '@angular/core';
import Story from 'src/app/models/story';
import { StoryService } from 'src/app/services/story-service/story.service';

@Component({
  selector: 'app-story',
  templateUrl: './story.component.html',
  styleUrls: ['./story.component.css'],
})
export class StoryComponent implements OnInit {
  public pageSize: number = 12;
  public pageNumber: number = 1;
  public stories: Story[] = [];
  public filterName: string = '';
  public isSearchingByName: boolean = false;

  constructor(private storyService: StoryService) {}

  ngOnInit(): void {
    this.stories = this.storyService.getStories(this.pageSize, this.pageNumber);
  }

  public updatePageSize(): void{
    this.stories = this.storyService.getStories(this.pageSize, this.pageNumber);
  }

  public previousPage() {
    this.pageNumber--;
    this.stories = this.storyService.getStories(this.pageSize, this.pageNumber);
  }

  public nextPage() {
    this.pageNumber++;
    this.stories = this.storyService.getStories(this.pageSize, this.pageNumber);
  }

  public goBackToAllStories(): void{
    this.isSearchingByName = false;
    this.stories = this.storyService.getStories(this.pageSize, this.pageNumber);
  }

  public searchByName(): void{
    if(this.filterName.length > 0){
      this.isSearchingByName = true;
      this.stories = this.storyService.getStoriesByName(this.filterName);
    }
  }
}
